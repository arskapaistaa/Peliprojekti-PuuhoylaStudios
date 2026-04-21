using Godot;
using System;

public partial class Counters : CanvasLayer
{
	[Export] public Label cheeseCounterLabel;
	[Export] public Label drillCounterLabel;
	[Export] public HBoxContainer BuiltDrill;
	[Export] private AnimationPlayer _animations = null;

    public override void _Ready()
    {
        GameManager.Instance.counters = this;
		BuiltDrill.Visible = false;
    }

	public void UpdateCheeseScore(int CheeseScore)
	{
		cheeseCounterLabel.Text = CheeseScore.ToString();

		// Play animation when collected
		_animations.Play("CheeseCollected");

	}

	public void UpdateDrillScore(int DrillScore)
	{
		drillCounterLabel.Text = DrillScore.ToString();

		// Play animation when collected
		_animations.Play("DrillCollected");
	}
}
