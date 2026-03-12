using Godot;
using System;

public partial class Drill : Collectable
{
	private int _drillScore = 1;

	protected override void Collect(Mouse mouse)
	{
		GD.Print("Drill collected");
		GameManager.Instance.AddDrill(_drillScore);
	}
}
