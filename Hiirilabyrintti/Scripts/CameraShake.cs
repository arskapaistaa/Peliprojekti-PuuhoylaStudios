using Godot;
using System;

public partial class CameraShake : Camera2D
{
	// Intense of shake.
	private float _shakeIntensity = 0.0f;

	// How long shake lasts.
	private float _shakeFade = 2.0f;

	// Starting point of camera.
	private Vector2 defaultOffset;

	public override void _Ready()
	{
		// Default offset is same as in editor.
		defaultOffset = Offset;
	}


	public override void _Process(double delta)
	{
		if (_shakeIntensity > 0)
		{
			_shakeIntensity = Mathf.Lerp(_shakeIntensity, 0, (float)(_shakeFade * delta));

			Offset = defaultOffset + new Vector2((float)GD.RandRange(-_shakeIntensity,_shakeIntensity),
												(float)GD.RandRange(-_shakeIntensity,_shakeIntensity));
		}
		else
		{
			Offset = defaultOffset;
		}
	}

	// Mehtod to call. Paramater as how intense shake you want and how long.
	public void Shake(float intensity)
	{
		_shakeIntensity = intensity;
	}
}
