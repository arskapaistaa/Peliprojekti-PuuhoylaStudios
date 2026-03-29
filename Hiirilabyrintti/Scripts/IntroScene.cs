using Godot;
using System;
using System.Threading;

public partial class IntroScene : Control
{
	[Export] private AnimationPlayer _animations;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
