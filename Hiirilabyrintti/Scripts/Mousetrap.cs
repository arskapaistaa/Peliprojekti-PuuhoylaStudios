using Godot;
using System;
using System.Collections;

public partial class Mousetrap : Area2D
{
	[Export] private AnimatedSprite2D _mouseTrap;
	[Export] private AudioStreamPlayer2D _mouseTrapSFX = null;
	[Export] private GpuParticles2D _cheese = null;
	[Export] private Timer _cooldown = null;
	bool _isActive = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		_cooldown.Timeout += OnTimerTimeout;
	}

    private void OnBodyEntered(Node2D body)
	{
		GD.Print("Entered: " + body.Name + " Type: " + body.GetType());

		if (body is Mouse && _isActive)
		{
			GD.Print("Mouse detected!");

			_mouseTrap.Play("default");
			_isActive = false;

			// Sound
			if (SettingsManager.Instance.Volume)
			{
				_mouseTrapSFX.Play();
			}

			// Particles
			if (GameManager.Instance.CheeseScore >= 3)
			{
				_cheese.Amount = 3;
				_cheese.Emitting = true;
			}
			else if (GameManager.Instance.CheeseScore < 3)
			{
				_cheese.Amount = GameManager.Instance.CheeseScore;
				_cheese.Emitting = true;
			}

			GameManager.Instance.RemoveCheese(3);

			GameManager.Instance.MouseCanMove = false;

			_cooldown.Start();


		}
	}
	    private void OnTimerTimeout()
    {
        GameManager.Instance.MouseCanMove = true;
    }
}
