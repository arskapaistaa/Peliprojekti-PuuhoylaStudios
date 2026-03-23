using Godot;
using System;

public partial class Cheese : Collectable
{
	private int _cheeseScore = 1;

	protected override void Collect(Mouse mouse)
	{
		GD.Print("Cheese collected");
		GameManager.Instance.AddCheese(_cheeseScore);
	}
}
