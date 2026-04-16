using Godot;
using System;

public partial class PuuhoylaScene : Control
{
	[Export] private AnimationPlayer _animations = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animations.Play("Start");
		_animations.AnimationFinished += onAnimationFinished;
	}
	public void onAnimationFinished(StringName animationName)
	{
		GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Game Scenes/Main_menu.tscn");
	}
}
