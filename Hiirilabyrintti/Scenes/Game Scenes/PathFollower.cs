using Godot;
using System;

public partial class PathFollower : Node
{
	[Export] private PathFollow2D _pathFollow;

	[Export] private float _movementSpeed = 2.0f;

	// Later can be used to start moving by changing false to true.
	[Export] public bool _canMove = true;

	public override void _Ready()
	{
		// Progress starts from beginning
		_pathFollow.Progress = 0.0f;
	}

	public override void _Process(double delta)
	{
		if(_canMove)
		{
			_pathFollow.ProgressRatio += _movementSpeed * (float) delta;
		}

		// When node reaches to the end
		if(_pathFollow.ProgressRatio >= 1f)
		{
			_canMove = false;
		}
	}
}
