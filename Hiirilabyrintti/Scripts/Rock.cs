using Godot;
using System;

public partial class Rock : Area2D
{
	[Export] private AnimationPlayer _animations;
	[Export] private Label rockMessageLabel;
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
		if (body is Mouse && !GameManager.Instance._hasDrill && GameManager.Instance.DrillScore < GameManager.Instance._neededToBuildDrill)
		{
			GD.Print("Player Entered");
			_animations.Play("PopUp");
			drillPromptShown = false;
		} else if (body is Mouse && !GameManager.Instance._hasDrill && GameManager.Instance.DrillScore >= GameManager.Instance._neededToBuildDrill)
		{
			GameManager.Instance.EmitSignal("DrillReady");
			drillPromptShown = true;
		} else if (body is Mouse && GameManager.Instance._hasDrill)
		{
			rockMessageLabel.Text = "With the drill, you can break\nthrough this rock and escape!";
			_animations.Play("PopUp");
			drillPromptShown = false;
		}
	}

	// Method when player exits the area.
	private void OnBodyExit(Node2D body)
	{
		if (body is Mouse && !drillPromptShown)
		{
			GD.Print("Player Exit");
			_animations.Play("PopOut");
		}
	}
}
