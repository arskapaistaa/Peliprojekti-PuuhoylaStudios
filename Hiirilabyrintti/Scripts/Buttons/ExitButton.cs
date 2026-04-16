using Godot;
using System;

public partial class ExitButton : Button
{
	[Export] private Button _exitButton = null;
	[Export] private AnimationPlayer _animations = null;
	[Export] private AudioStreamPlayer2D _buttonDownSFX = null;

	public override void _Ready()
	{
		_exitButton.Pressed += ButtonPressed;
		_animations.AnimationFinished += onAnimationFinished;
	}

    public void ButtonPressed()
	{
		GameManager.Instance._hasDrill = false;
		GameManager.Instance.CheeseScore = 0;
		GameManager.Instance.DrillScore = 0;
		GameManager.Instance.EtsijaScore = 0;
		GameManager.Instance.EtenijaScore = 0;
		GameManager.Instance.EdistajaScore = 0;

		_animations.Play("FadeOut");

		if (SettingsManager.Instance.Volume)
		{
			_buttonDownSFX.Play();
		}

		Input.VibrateHandheld(100);
	}

	    private void onAnimationFinished(StringName animName)
    {
        if (animName == "FadeOut")
		{
			GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Game Scenes/Main_menu.tscn");
		}
    }
}
