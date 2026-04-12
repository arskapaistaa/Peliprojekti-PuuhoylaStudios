using Godot;
using System;
using System.ComponentModel;

public partial class Rock : Area2D
{
	[Signal]
	public delegate void DrillStartedEventHandler();
	[Signal]
	public delegate void RockDestroyedEventHandler();

	[Export] private AnimationPlayer _animations;
	[Export] private Label rockMessageLabel;
	[Export] private Button _drillButton;
	[Export] private AnimatedSprite2D _rockSprite;
	private Mouse _mouse;
	private bool drillingStarted = false;

	public override void _Ready()
	{
		_drillButton.Visible = false;
		_drillButton.Pressed += OnDrillPressed;
		_rockSprite.AnimationFinished += OnAnimationFinished;
	}

	public override void _EnterTree()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExit;
	}

	public override void _ExitTree()
	{
		BodyEntered -= OnBodyEntered;
		BodyExited -= OnBodyExit;
	}

	private bool drillPromptShown = false;

	// Method when player enters the area.
	private void OnBodyEntered(Node2D body)
	{
		if (body is not Mouse mouse)
		{
			return;
		}

		_mouse = mouse; // store reference

		// ✅ connect signal ONLY ONCE
		if (!IsConnected(SignalName.DrillStarted, new Callable(mouse, nameof(Mouse.StartDrilling))))
		{
			Connect(SignalName.DrillStarted, new Callable(mouse, nameof(Mouse.StartDrilling)));
		}

		GD.Print("Mouse entered rock area");

		if (!GameManager.Instance._hasDrill)
		{
			if (GameManager.Instance.DrillScore >= GameManager.Instance._neededToBuildDrill)
			{
				GameManager.Instance.EmitSignal("DrillReady");
				drillPromptShown = true;
			}
			else
			{
				_animations.Play("PopUp");
				drillPromptShown = false;
			}
		}
		else
		{
			GD.Print("Player HAS drill");

			rockMessageLabel.Text = "With the drill, you can break\nthrough this rock and escape!";
			rockMessageLabel.Visible = true;

			_drillButton.Visible = true;

			_animations.Play("PopUp");

			drillPromptShown = false;
		}
		if (!IsConnected(SignalName.RockDestroyed, new Callable(mouse, nameof(Mouse.StopDrilling))))
		{
			Connect(SignalName.RockDestroyed, new Callable(mouse, nameof(Mouse.StopDrilling)));
		}
	}

	private void OnDrillPressed()
	{
		GD.Print("Drill button pressed");

		EmitSignal(SignalName.DrillStarted);
		drillingStarted = true;
		_rockSprite.Play("Drilling");
		GameManager.Instance._mouseCanMove = false;
		_animations.Play("PopOut");
	}

	// Method when player exits the area.
	private void OnBodyExit(Node2D body)
	{
		if (drillingStarted)
		{
			return;
		} else if (body is Mouse && !drillPromptShown)
		{
			GD.Print("Player Exit");
			_animations.Play("PopOut");
		}
	}

	private void OnAnimationFinished()
	{
		if (_rockSprite.Animation == "Drilling")
		{
			EmitSignal(SignalName.RockDestroyed);
			GameManager.Instance._mouseCanMove = true;
			GetParent().QueueFree();
		}
	}
}