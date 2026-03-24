using Godot;
using System;
public partial class Exit : Area2D
{

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	// Simple method to exit labyrinth when player enters area2D
	private void OnBodyEntered(Node2D body)
	{
		// Load end game scene when enter area2D
		if (body is CharacterBody2D)
		{
			GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Game Scenes/end_game_scene.tscn");
		}
	}
}
