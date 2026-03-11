using Godot;
using System;

public partial class CheeseCounter : CanvasLayer
{
	[Export] public Label counterLabel;

	public void UpdateScore(int CheeseScore)
	{
		counterLabel.Text = CheeseScore.ToString();
	}
}
