using Godot;
using System;

public partial class GameManager : Node
{

	public static GameManager Instance
	{
		get;
		private set;
	}

	public GameManager()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			// Jos singleton-olio on jo olemassa, tuhotaan luotu olio.
			QueueFree();
			return;
		}
	}

	// Cheese part
	private int _cheeseScore = 0;

	public int CheeseScore
	{
		get {return _cheeseScore; }
		set
		{
			// Min and Max cheese score
			_cheeseScore = Mathf.Clamp(value, 0, 999);
			// For Debugging
			GD.Print("Cheese Score:" + _cheeseScore);
		}
	}

	// Method to call from cheese class
	public bool AddCheese(int amount)
	{
		if (amount < 0)
		{
			return false;
		}

		CheeseScore += amount;
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
		return true;
	}

}
