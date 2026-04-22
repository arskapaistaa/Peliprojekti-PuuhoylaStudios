using Godot;
using System;

public partial class AudioManagerMainMenu : Node
{

	[Export] private AudioStreamPlayer _music = null;
	[Export] private Button _musicButton = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		_musicButton.Pressed += OnButtonPressed;

		if (SettingsManager.Instance.Music)
		{
			_music.Play();
		}
		else
		{
			_music.Playing = false;
		}

	}

    private void OnButtonPressed()
    {
        if (SettingsManager.Instance.Music)
		{
			_music.Play();
		}
		else
		{
			_music.Playing = false;
		}
    }
}
