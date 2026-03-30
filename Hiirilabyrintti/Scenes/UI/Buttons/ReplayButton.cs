using Godot;
using System;

public partial class ReplayButton : Button
{
	[Export] private Button _replay = null;

	public override void _Ready()
	{
		_replay.Pressed += ButtonPressed;
	}

	public void ButtonPressed()
	{
		GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Game Scenes/MainScene.tscn");

		// Reset everything
		GameManager.Instance.CheeseScore = 0;
		GameManager.Instance.DrillScore = 0;
		GameManager.Instance.EtsijaScore = 0;
		GameManager.Instance.EtenijaScore = 0;
		GameManager.Instance.EdistajaScore = 0;

		// TODO: Load different scene
	}
}
