using Godot;
using System;

public partial class Rock : Area2D
{
	[Export] private AnimationPlayer _animations;
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
		if (body is Mouse)
		{
			GD.Print("Player Entered");
			_animations.Play("PopUp");
		}
	}

	// Method when player exits the area.
	private void OnBodyExit(Node2D body)
	{
		if (body is Mouse)
		{
			GD.Print("Player Exit");
			_animations.Play("PopOut");
		}
	}
}
