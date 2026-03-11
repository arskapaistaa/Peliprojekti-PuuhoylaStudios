using Godot;
using System;

public partial class MainMenu : Node
{
	[Export] private Button _playButton;
	[Export] private AnimationPlayer _animations;
	[Export] private Timer _animationTimer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// // Connect pressed signal to method.
		_playButton.Pressed += ButtonPressed;
	}
	private void ButtonPressed()
	{
		// Disable the button to prevent multiple presses.
		_playButton.Disabled = true;

		// Play fade out animation and after it load the main scene.
		_animations.Play("FadeOut");
		// Start timer
		_animationTimer.Start();

		// Load new scene after Timeout
		_animationTimer.Timeout += LoadMainScene;
	}
	private void LoadMainScene()
	{
		// Load new scene and remove main menu scene.
		GetTree().ChangeSceneToFile("res://Scenes/Game Scenes/MainScene.tscn");
	}
}
