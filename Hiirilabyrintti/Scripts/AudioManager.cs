using Godot;
using System;

public partial class AudioManager : Node
{
	[Export] private AudioStreamPlayer2D _waterDropsSFX = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_waterDropsSFX.Playing = SettingsManager.Instance.Volume;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
