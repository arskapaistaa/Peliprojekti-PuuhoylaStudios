using Godot;
using System;

public partial class ReplayButton : Button
{
	[Export] private Button _replay = null;
	[Export] private AudioStreamPlayer2D _buttonDownSFX = null;

	public override void _Ready()
	{
		_replay.Pressed += ButtonPressed;
	}

	public void ButtonPressed()
	{
		GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Game Scenes/IntroScene.tscn");

		// Reset everything
		GameManager.Instance.CheeseScore = 0;
		GameManager.Instance.DrillScore = 0;
		GameManager.Instance.EtsijaScore = 0;
		GameManager.Instance.EtenijaScore = 0;
		GameManager.Instance.EdistajaScore = 0;

		if (SettingsManager.Instance.Volume)
		{
			_buttonDownSFX.Play();
		}

		Input.VibrateHandheld(100);

		// TODO: Load different scene
	}
}
