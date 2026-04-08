using Godot;
using System;

public partial class SettingsManager : Node
{

	public static SettingsManager Instance
	{
		get;
		private set;
	}

	public SettingsManager()
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

    // 0 = FIN, 1 = ENG
    // Why int? Because String array 0 = FIN and 1 = ENG. When multiple choises, use different arrays.
    private int _language = 0;
    public int Language
    {
        get { return _language; }
        set
        {
            _language = Mathf.Clamp(value, 0, 1);
        }
    }

	private bool _volume = true;
	public bool Volume
	{
		get {return _volume; }
		set { _volume = value; }
	}
}