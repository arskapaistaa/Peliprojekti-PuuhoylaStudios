using Godot;
using System;

public partial class ExitButton : Button
{
	[Export] private Button _exit = null;

	public override void _Ready()
	{
		_exit.Pressed += ButtonPressed;
	}

	public void ButtonPressed()
	{
		GetTree().Quit();
	}
}
