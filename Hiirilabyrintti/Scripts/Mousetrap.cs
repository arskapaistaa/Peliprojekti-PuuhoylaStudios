using Godot;
using System;
using System.Collections;

public partial class Mousetrap : Area2D
{

	[Export] private AnimatedSprite2D _mouseTrap;
	bool _isActive = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBodyEntered(Node2D body)
	{
		GD.Print("Entered: " + body.Name + " Type: " + body.GetType());

		if (body is Mouse && _isActive)
		{
			GD.Print("Mouse detected!");
			GameManager.Instance.RemoveCheese(3);
			_mouseTrap.Play("default");
			_isActive = false;
		}
	}

}
