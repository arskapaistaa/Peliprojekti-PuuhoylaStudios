using Godot;
using System;

public partial class MainMenu : Node
{
	[Export] private Button _playButton;
	[Export] private AnimationPlayer _animations;

	public override void _Ready()
	{
		_animations.Play("Start");

		_playButton.Disabled = false;

		// Connect pressed signal to method.
		_playButton.Pressed += ButtonPressed;

		_animations.AnimationFinished += onAnimationFinished;
	}

    private void ButtonPressed()
	{
		// Disable the button to prevent multiple presses.
		_playButton.Disabled = true;

		// Play fade out animation and after it load the main scene.
		_animations.Play("FadeOut");

	}
	private void onAnimationFinished(StringName animName)
    {
        if(animName == "FadeOut")
		{
		// Load new scene and remove main menu scene.
		GetTree().ChangeSceneToFile("res://Scenes/Game Scenes/IntroScene.tscn");
		}
    }
}
