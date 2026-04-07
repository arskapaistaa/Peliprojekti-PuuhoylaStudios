using Godot;

public partial class SettingsMenu : Control
{
	[Export] private Button _volumeButton = null;

	public override void _Ready()
	{
		_volumeButton.Pressed += VolumeButtonPressed;
		UpdateVolumeButton();
	}


	public void VolumeButtonPressed()
	{
		// Basic volume operation. Set false if button is true and so on...
		SettingsManager.Instance.Volume = !SettingsManager.Instance.Volume;
		// Update text
		UpdateVolumeButton();
		// For debugging
		GD.Print(SettingsManager.Instance.Volume ? "Volume On" : "Volume Off");
	}

	// Set text to ON or OFF
	public void UpdateVolumeButton()
	{
		// Basicly if and else.
		// ? = True or false.
		_volumeButton.Text = SettingsManager.Instance.Volume ? "ON" : "Off";
	}
}
