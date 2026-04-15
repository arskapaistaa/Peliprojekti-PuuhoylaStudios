using Godot;
using System;

public partial class MainMenu : Node
{
	[Export] private Button _playButton;
	[Export] private Button _creditsButton = null;
	[Export] private AnimationPlayer _animations;

	public bool _credits = false;

	public override void _Ready()
	{
		_animations.Play("Start");

		_playButton.Disabled = false;

		// Connect pressed signal to method.
		_playButton.Pressed += PlayButtonPressed;
		_creditsButton.Pressed += CreditsButtonPressed;

		_animations.AnimationFinished += onAnimationFinished;
	}

    private void PlayButtonPressed()
	{
		// Disable the button to prevent multiple presses.
		_playButton.Disabled = true;

		// Play fade out animation and after it load the main scene.
		_animations.Play("FadeOut");

	}


		// Load intro scene.
	    private void CreditsButtonPressed()
    {
		if(!_credits)
		{
			_animations.Play("Credits");
			_credits = true;
		}
		else
		{
			_animations.Play("CreditsOff");
			_credits = false;
		}

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
