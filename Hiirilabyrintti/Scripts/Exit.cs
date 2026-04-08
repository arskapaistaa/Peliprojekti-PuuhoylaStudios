using Godot;
using System;
public partial class Exit : Area2D
{
	[Export] private AnimationPlayer _animations;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		_animations.AnimationFinished += onAnimationFinished;
	}

	// Simple method to exit labyrinth when player enters area2D
	private void OnBodyEntered(Node2D body)
	{
		// Load end game scene when enter area2D
		if (body is CharacterBody2D)
		{
			_animations.Play("FadeOut");

		}
	}
		public void onAnimationFinished(StringName animationName)
	{
		if (animationName == "FadeOut")
		{
			GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Game Scenes/end_game_scene.tscn");
		}
	}
}
