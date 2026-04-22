using Godot;
using System;

public partial class AudioManagerEndGameScene : Node
{

	[Export] private AudioStreamPlayer _music = null;
	[Export] private AudioStreamPlayer2D _labraRat = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		if (SettingsManager.Instance.Music)
		{
			_music.Play();
		}

		if (SettingsManager.Instance.Volume)
		{
			_labraRat.VolumeDb = 0;
		}
		else
		{
			_labraRat.VolumeDb = -60;
		}
	}


}
