using System;
using System.Formats.Asn1;
using Godot;

public partial class Collectable : Area2D
{
	private bool _isCollected = false;

	public bool IsCollected
	{
		get { return _isCollected; }
	}

	public override void _EnterTree()
	{
		BodyEntered += OnBodyEntered;
	}
	public override void _ExitTree()
	{
		BodyEntered -= OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Mouse mouse)
		{
			_isCollected = true;
			QueueFree();
			Collect(mouse);

		}
	}

	protected virtual void Collect(Mouse mouse)
	{

	}
}
