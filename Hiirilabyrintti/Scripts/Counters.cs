using Godot;
using System;

public partial class Counters : CanvasLayer
{
	[Export] public Label cheeseCounterLabel;
	[Export] public Label drillCounterLabel;
	[Export] public HBoxContainer BuiltDrill;

    public override void _Ready()
    {
        GameManager.Instance.counters = this;
		BuiltDrill.Visible = false;
    }

	public void UpdateCheeseScore(int CheeseScore)
	{
		cheeseCounterLabel.Text = CheeseScore.ToString();
	}

	public void UpdateDrillScore(int DrillScore)
	{
		drillCounterLabel.Text = DrillScore.ToString();
	}
}
