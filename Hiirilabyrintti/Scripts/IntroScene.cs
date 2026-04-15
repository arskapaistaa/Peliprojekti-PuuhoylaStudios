using Godot;
using System;
using System.Threading;

public partial class IntroScene : Control
{
	[Export] private String[] _dialogArray = {};
	[Export] private Label _dialogLabel;
	[Export] private AnimationPlayer _animations;
	private int _randomMap = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		_dialogLabel.Text = _dialogArray[SettingsManager.Instance.Language];

		_animations.Play("Start");
		_animations.AnimationFinished += onAnimationFinished;

	}

	public void onAnimationFinished(StringName animationName)
	{
		if (animationName == "Start")
		{
			// Random number between 1 - 2
			_randomMap = GD.RandRange(1, 3);
			GD.Print("Random map: " + _randomMap);

			// Load new scene. Random number decides which.
			switch (_randomMap)
			{
				// Load Main scene 1
				case 1:
				GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Game Scenes/MainScene.tscn");
				break;

				// Load Main scene 2
				case 2:
				GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Game Scenes/MainScene2.tscn");
				break;

				// Load Main scene 3
				case 3:
				GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Game Scenes/MainScene3.tscn");
				break;

				// TODO: Scene 4 and 5!
			}
		}
	}
}
