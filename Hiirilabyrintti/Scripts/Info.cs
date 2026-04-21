using Godot;
using System;

public partial class Info : Button
{
	[Export] private AudioStreamPlayer _buttonDownSFX = null;
	[Export] private AnimationPlayer _animations = null;
	[Export] private Label _infoLabel = null;
	private bool _info = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Pressed += OnButtonPressed;

	}

    private void OnButtonPressed()
    {
		if(SettingsManager.Instance.Language == 0)
		{
			_infoLabel.Text = "Edistäjä\nPohdit yrittäjyyttä. Sinulla on idea, tavoite tai kipinä. Kaipaat kumminkin rohkaisua ja tukea jotta pääset tavoitteeseesi.\n \n Etenijä\nOlet tavoitteellinen ja valmis etenemään polullasi. Tiedät suuntasi mutta kaipaat apua päästäksesi tavoitteeseesi. \n \n Etsijä\nOlet oman suuntasi etsijä. Saatat vielä pohtia identiteettiäsi, omia arvojasi ja elämän suuntaa.";
		}

        if(!_info)
		{
			_animations.Play("InfoOn");
			_info = true;
		}
		else
		{
			_animations.Play("InfoOff");
			_info = false;
		}


		if (SettingsManager.Instance.Volume)
		{
			_buttonDownSFX.Play();
		}

		Input.VibrateHandheld(100);
    }
}
