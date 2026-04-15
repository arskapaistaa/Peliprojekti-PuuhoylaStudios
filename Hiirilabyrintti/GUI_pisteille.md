# GUI Pisteet
Teimme md tiedoston, koska olimme jo aiemmin tehneet logiikan GUI pisteiden toimivuudelle.

## Game manager
	// To update GUI
	[Export] public Counters counters;

	// Method to call from cheese class
	public bool AddCheese(int amount)
	{
		if (amount < 0)
		{
			return false;
		}

		CheeseScore += amount;

		// Update UI
		if (counters != null)
		{
			counters.UpdateCheeseScore(CheeseScore);

		}
		else
		{
			GD.Print("Counter missing!");
		}

		return true;
	}

	// Drill part
	private int _drillScore = 0;
	public int DrillScore
	{
		get {return _drillScore; }
		set
		{
			// Min and Max drill score
			_drillScore = Mathf.Clamp(value, 0, 999);

			// For Debugging
			GD.Print("Drill Score:" + _drillScore);
		}
	}

	// Method to call from drill class
	public bool AddDrill(int amount)
	{
		if (amount < 0)
		{
			return false;
		}

		DrillScore += amount;

		// Update UI
		if (counters != null)
		{
			counters.UpdateDrillScore(DrillScore);

		}
		else
		{
			GD.Print("Counter missing!");
		}
		return true;
	}

## Counters
using Godot;
using System;

public partial class Counters : CanvasLayer
{
	[Export] public Label cheeseCounterLabel;
	[Export] public Label drillCounterLabel;

	public override void _Ready()
	{
		GameManager.Instance.counters = this;
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


## Commit
GUI Pisteille

## Osallistujat
Joonas Björninen - Kirjuri
Arvo Koskinen
Tomi Mäkelä
Suvi Käyhkö
Aune Koskinen
