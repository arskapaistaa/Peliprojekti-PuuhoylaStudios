using Godot;
using System;

public partial class GameManager : Node
{
	[Signal]
	public delegate void DrillReadyEventHandler();
	private bool _mouseCanMove = true;
	public bool MouseCanMove
	{
		get { return _mouseCanMove; }
		set
		{
			_mouseCanMove = value;
			GD.Print("Mouse can move: " + _mouseCanMove);
		}
	}



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

	public bool RemoveCheese(int amount)
	{
		if (amount < 0 || amount > CheeseScore)
		{
			return false;
		}

		CheeseScore -= amount;
		if (CheeseScore < 0)
		{
			CheeseScore = 0;
		}

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
	public int _neededToBuildDrill = 1;
	public bool _hasDrill = false;
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

		if (DrillScore >= _neededToBuildDrill && !_hasDrill)
		{
			GD.Print("You have enough drill parts to build the drill!");
			EmitSignal(SignalName.DrillReady);
		}
		return true;
	}

	public bool RemoveDrill(int amount)
	{
		if (amount < 0 || amount > DrillScore)
		{
			return false;
		}

		DrillScore -= amount;
		if (DrillScore < 0)
		{
			DrillScore = 0;
		}

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

	// Etsijä part
	private int _etsijaScore = 0;
	public int EtsijaScore
	{
		get { return _etsijaScore; }
		set
		{
			// Min and max. Max now 100 = 10 questions?
			_etsijaScore = Mathf.Clamp(value, 0 , 100);

			// For debugging
			GD.Print("Etsijä score:" + _etsijaScore);
		}
	}

	public bool AddEtsija(int amount)
	{
		if (amount < 0 )
		{
			return false;
		}
		EtsijaScore += amount;
		return true;
	}

	private int _etenijaScore = 0;
	public int EtenijaScore
	{
		get { return _etenijaScore; }
		set
		{
			// Min and max. Max now 100 = 10 questions?
			_etenijaScore = Mathf.Clamp(value, 0 , 100);

			// For debugging
			GD.Print("Etenijä score:" + _etenijaScore);
		}
	}

	public bool AddEtenija(int amount)
	{
		if (amount < 0 )
		{
			return false;
		}
		EtenijaScore += amount;
		return true;
	}

	private int _edistajaScore = 0;
	public int EdistajaScore
	{
		get { return _edistajaScore; }
		set
		{
			// Min and max. Max now 100 = 10 questions?
			_edistajaScore = Mathf.Clamp(value, 0 , 100);

			// For debugging
			GD.Print("Edistäjä score:" + _edistajaScore);
		}
	}

	public bool AddEdistaja(int amount)
	{
		if (amount < 0 )
		{
			return false;
		}
		EdistajaScore += amount;
		return true;
	}
}
