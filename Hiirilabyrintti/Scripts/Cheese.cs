using Godot;
using System;

public partial class Cheese : Collectable
{
	private int _cheeseScore = 1;
	[Export] private CanvasLayer _canvasLayer;

	protected override void Collect(Mouse mouse)
	{
		GD.Print("Cheese collected");
		GameManager.Instance.AddCheese(_cheeseScore);

		//var label = GetNode<Label>(World/CheeseCounter/HBoxContainer/Label);
		//label.Text = GameManager.Instance.CheeseScore.ToString();
		// TODO loppuun!
	}
}
