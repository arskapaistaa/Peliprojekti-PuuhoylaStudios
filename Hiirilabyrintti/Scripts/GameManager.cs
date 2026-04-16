using Godot;
using System;

public partial class GameManager : Node
{
	[Signal]
	public delegate void DrillReadyEventHandler();

	public bool _mouseCanMove = true;

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
			QueueFree();
			return;
		}
	}

	// Cheese part
	private int _cheeseScore = 0;

	public int CheeseScore
	{
		get { return _cheeseScore; }
		set
		{
			_cheeseScore = Mathf.Clamp(value, 0, 999);
			GD.Print("Cheese Score:" + _cheeseScore);
		}
	}

	[Export] public Counters counters;

	public bool AddCheese(int amount)
	{
		if (amount < 0)
			return false;

		CheeseScore += amount;

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
		if (amount < 0)
			return false;

		CheeseScore -= amount;

		if (CheeseScore < 0)
			CheeseScore = 0;

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
	public int _neededToBuildDrill = 3;
	public bool _hasDrill = false;

	public int DrillScore
	{
		get { return _drillScore; }
		set
		{
			_drillScore = Mathf.Clamp(value, 0, 999);
			GD.Print("Drill Score:" + _drillScore);
		}
	}

	public bool AddDrill(int amount)
	{
		if (amount < 0)
			return false;

		DrillScore += amount;

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
			return false;

		DrillScore -= amount;

		if (DrillScore < 0)
			DrillScore = 0;

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
			_etsijaScore = Mathf.Clamp(value, 0, 100);
			GD.Print("Etsijä score:" + _etsijaScore);
		}
	}

	public bool AddEtsija(int amount)
	{
		if (amount < 0)
			return false;

		EtsijaScore += amount;
		return true;
	}

	// Etenijä
	private int _etenijaScore = 0;

	public int EtenijaScore
	{
		get { return _etenijaScore; }
		set
		{
			_etenijaScore = Mathf.Clamp(value, 0, 100);
			GD.Print("Etenijä score:" + _etenijaScore);
		}
	}

	public bool AddEtenija(int amount)
	{
		if (amount < 0)
			return false;

		EtenijaScore += amount;
		return true;
	}

	// Edistäjä
	private int _edistajaScore = 0;

	public int EdistajaScore
	{
		get { return _edistajaScore; }
		set
		{
			_edistajaScore = Mathf.Clamp(value, 0, 100);
			GD.Print("Edistäjä score:" + _edistajaScore);
		}
	}

	public bool AddEdistaja(int amount)
	{
		if (amount < 0)
			return false;

		EdistajaScore += amount;
		return true;
	}
}