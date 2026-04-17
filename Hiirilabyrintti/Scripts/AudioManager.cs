using Godot;
using System;

public partial class AudioManager : Node
{
	[Export] private AudioStreamPlayer2D _waterDropsSFX = null;
	[Export] private AudioStreamPlayer _music = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (SettingsManager.Instance.Volume)
		{
			_waterDropsSFX.Play();
		}

		if (SettingsManager.Instance.Music)
		{
			_music.Play();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
