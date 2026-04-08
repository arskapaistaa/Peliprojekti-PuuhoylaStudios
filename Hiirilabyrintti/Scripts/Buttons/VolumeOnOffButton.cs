using Godot;
using System;

public partial class VolumeOnOffButton : Button
{
	[Export] private Button _volumeButton = null;

	public override void _Ready()
	{
		_volumeButton.Pressed += VolumeButtonPressed;
	}


	public void VolumeButtonPressed()
	{
		// Basic volume operation. Set false if button is true and so on...
		if (SettingsManager.Instance.Volume == true)
		{
			GD.Print("Volume off");
			SettingsManager.Instance.Volume = false;
		}
		else if (SettingsManager.Instance.Volume == false)
		{
			GD.Print("Volume On");
			SettingsManager.Instance.Volume = true;
		}
	}
}
