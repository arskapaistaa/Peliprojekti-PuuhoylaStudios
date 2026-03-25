using Godot;
using System;

public partial class CameraShake : Camera2D
{
	// Intense of shake.
	private float shakeIntensity = 0.0f;

	// How long shake lasts.
	private float shakeFade = 2.0f;

	// Starting point of camera.
	private Vector2 defaultOffset;

	public override void _Ready()
	{
		// Default offset is same as in editor.
		defaultOffset = Offset;
	}


	public override void _Process(double delta)
	{
		if (shakeIntensity > 0)
		{
			shakeIntensity = Mathf.Lerp(shakeIntensity, 0, (float)(shakeFade * delta));

			Offset = defaultOffset + new Vector2((float)GD.RandRange(-shakeIntensity,shakeIntensity),
												(float)GD.RandRange(-shakeIntensity, shakeIntensity));
		}
		else
		{
			Offset = defaultOffset;
		}
	}

	// Mehtod to call. Paramater as how intense shake you want.
	public void Shake(float intensity)
	{
		shakeIntensity = intensity;
	}
}
