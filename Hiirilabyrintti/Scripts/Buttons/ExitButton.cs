using Godot;
using System;

public partial class ExitButton : Button
{
	[Export] private Button _exitButton = null;
	[Export] private AnimationPlayer _animations = null;

	public override void _Ready()
	{
		_exitButton.Pressed += ButtonPressed;
		_animations.AnimationFinished += onAnimationFinished;
	}

    public void ButtonPressed()
	{
		_animations.Play("FadeOut");
	}

	    private void onAnimationFinished(StringName animName)
    {
        if (animName == "FadeOut")
		{
			GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Game Scenes/Main_menu.tscn");
		}
    }
}
