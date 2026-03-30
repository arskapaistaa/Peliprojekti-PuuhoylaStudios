using Godot;
using System;
using System.Threading;

public partial class IntroScene : Control
{
	[Export] private String[] _dialogArray = {};
	[Export] private Label _dialogLabel;
	[Export] private AnimationPlayer _animations;
	[Export] private LineEdit _playerName;
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
			GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Game Scenes/MainScene.tscn");
		}
	}
}
