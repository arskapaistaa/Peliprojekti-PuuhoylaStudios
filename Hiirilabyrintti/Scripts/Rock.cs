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
	[Export] private string[] _rockMessageLabelArray = {};
	[Export] private Button _drillButton;
	[Export]private string[] _drillButtonArray = {};
	[Export] private AnimatedSprite2D _rockSprite;
	[Export] private AudioStreamPlayer _buttonDownSFX = null;

	private Muskrat muskrat;
	private Mouse _mouse;
	private bool drillingStarted = false;
	private bool _canTrigger = false;
	private bool drillPromptShown = false;

	public override void _Ready()
	{
		//Language
		rockMessageLabel.Text = _rockMessageLabelArray[SettingsManager.Instance.Language];
		_drillButton.Text = _drillButtonArray[SettingsManager.Instance.Language];

		_drillButton.Visible = false;
		_drillButton.Pressed += OnDrillPressed;

		_rockSprite.AnimationFinished += OnAnimationFinished;

		GetTree().CreateTimer(0.2f).Timeout += () =>
		{
			_canTrigger = true;
		};
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

	// Method when player enters the area.
	private void OnBodyEntered(Node2D body)
	{
		if (!_canTrigger) return;

		if (body is Mouse mouse)
		{
			_mouse = mouse;

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
		else if (body is Muskrat m)
		{
			muskrat = m;

			GD.Print("Muskrat entered rock area");

			muskrat._drillSprite.Visible = true;
			muskrat._drillSprite.Play("default");

			_rockSprite.Play("Drilling");
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

		if (SettingsManager.Instance.Volume)
		{
			_buttonDownSFX.Play();
		}

		Input.VibrateHandheld(500);
	}

	// Method when player exits the area.
	private void OnBodyExit(Node2D body)
	{
		if (drillingStarted)
		{
			return;
		}
		else if (body is Mouse && !drillPromptShown)
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