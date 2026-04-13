using System;
using Godot;

public partial class SettingsMenu : Control
{
	[Export] private Button _volumeButton = null;
	[Export] private Button _musicButton = null;
	[Export] private Button _languageButton = null;
	[Export] private Button _creditsButton = null;

	public override void _Ready()
	{
		_volumeButton.Pressed += VolumeButtonPressed;
		_musicButton.Pressed += MusicButtonPressed;
		_languageButton.Pressed += LanguageButtonPressed;
		UpdateButtons();
	}

    public void VolumeButtonPressed()
	{
		// Basic volume operation. Set false if button is true and so on...
		SettingsManager.Instance.Volume = !SettingsManager.Instance.Volume;
		// Update text
		UpdateButtons();
		// For debugging
		GD.Print(SettingsManager.Instance.Volume ? "Volume On" : "Volume Off");
	}
		public void MusicButtonPressed()
    {
		SettingsManager.Instance.Music = !SettingsManager.Instance.Music;

		UpdateButtons();

    }
	    public void LanguageButtonPressed()
	{
		if (SettingsManager.Instance.Language == 0)
		{
			SettingsManager.Instance.Language = 1;
		}
		else
		{
			SettingsManager.Instance.Language = 0;
		}

		UpdateButtons();
	}

	// Set text to ON or OFF
	public void UpdateButtons()
	{
		// Basiclyif and else.
		// ? = True or false.
		// Load icon from path.
		_volumeButton.Icon = SettingsManager.Instance.Volume ? GD.Load<Texture2D>("res://Sprites/volume_on.png") : GD.Load<Texture2D>("res://Sprites/volume_off1.png");

		//Music button
		_musicButton.Icon = SettingsManager.Instance.Music ? GD.Load<Texture2D>("res://Sprites/music_on.png") : GD.Load<Texture2D>("res://Sprites/music_off.png");

		if (SettingsManager.Instance.Language == 0)
		{
			_languageButton.Icon = GD.Load<Texture2D>("res://Sprites/fin_off.png");
		}
		else
		{
			_languageButton.Icon = GD.Load<Texture2D>("res://Sprites/uk_off.png");
		}
	}
}