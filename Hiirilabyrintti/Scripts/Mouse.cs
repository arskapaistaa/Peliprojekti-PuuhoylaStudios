using Godot;
using System;

public partial class Mouse : CharacterBody2D
{
    [Export] public float speed = 160f;
    [Export] private float _slowedSpeed = 50.0f;
    [Export] private GpuParticles2D _walkParticles;
     [Export] private GpuParticles2D _drillParticles;

    // For animations
    [Export] private AnimatedSprite2D _sprite;

    [Export] private CameraShake _camera;
    private bool wasOnWall = false;

    [Export] private Control DrillPrompt;
    [Export] private Button YesButton;
    [Export] private Button NoButton;
    [Export] private AnimatedSprite2D _drill;
    [Export] private string[] _yesButtonArray = {};
    [Export] private string[] _noButtonArray = {};
    [Export] private Label _drillPromptLabel = null;
    [Export] private string[] _drillPrompArray = {};
    [Export] private AudioStreamPlayer2D _walkSFX = null;
	[Export] private AudioStreamPlayer _buttonDownSFX = null;
    [Export] private AudioStreamPlayer _drillSFX = null;
    [Export] private AudioStreamPlayer _hitSFX = null;


    private int neededToBuildDrill = GameManager.Instance._neededToBuildDrill;

    public override void _Ready()
    {
        YesButton.Pressed += OnYesPressed;
        NoButton.Pressed += OnNoPressed;

        GameManager.Instance.Connect(
            GameManager.SignalName.DrillReady,
            new Callable(this, nameof(ShowDrillPrompt))
        );

        DrillPrompt.Visible = false;
        _drill.Visible = false;

    }

    public override void _PhysicsProcess(double delta)
    {
        if (!GameManager.Instance.MouseCanMove)
        {
            speed = 0f;
        }
        else if (IsOnWall())
        {
            speed = _slowedSpeed;
        }
        else
        {
            speed = 160f;
        }

        Vector2 inputDirection = new Vector2(
            Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
            Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up"));

        Velocity = inputDirection * speed;

        if (Velocity.Length() > 0)
        {
            Rotation = Velocity.Angle() + Mathf.Pi / 2f;

            _sprite.Play("Move");
            _walkParticles.Emitting = true;
        }
        else
        {
            _sprite.Play("Idle");
            _walkParticles.Emitting = false;
        }

        // Camera shake on wall impact
        bool isOnWall = IsOnWall();
        if (isOnWall && !wasOnWall)
        {
            _camera.Shake(2.0f);
            Input.VibrateHandheld(200);

            if (SettingsManager.Instance.Volume)
            {
                _hitSFX.Play();
            }
        }
        wasOnWall = isOnWall;

        MoveAndSlide();
    }

    private void ShowDrillPrompt()
    {
        YesButton.Text = _yesButtonArray[SettingsManager.Instance.Language];
        NoButton.Text = _noButtonArray[SettingsManager.Instance.Language];
        _drillPromptLabel.Text = _drillPrompArray[SettingsManager.Instance.Language];

        GameManager.Instance.MouseCanMove = false;
        DrillPrompt.Visible = true;
    }

    private void OnYesPressed()
    {
        GameManager.Instance.RemoveDrill(neededToBuildDrill);
        DrillPrompt.Visible = false;
        GameManager.Instance.MouseCanMove = true;
        GameManager.Instance._hasDrill = true;
        GameManager.Instance.counters.BuiltDrill.Visible = true;

        if (SettingsManager.Instance.Volume)
		{
			_buttonDownSFX.Play();
		}

		Input.VibrateHandheld(500);
    }

    private void OnNoPressed()
    {
        DrillPrompt.Visible = false;
        GameManager.Instance.MouseCanMove = true;

        if (SettingsManager.Instance.Volume)
		{
			_buttonDownSFX.Play();
		}

		Input.VibrateHandheld(500);
    }

    public void StartDrilling()
    {
        _drill.Visible = true;
        _drill.Play("drill");

        _drillParticles.Emitting = true;

        if (SettingsManager.Instance.Volume)
		{
			_drillSFX.Play();
		}

        _camera.Shake(8.0f);
        Input.VibrateHandheld(3000);
    }

    public void StopDrilling()
    {
        _drill.Visible = false;
        _drill.Stop();

        _drillParticles.Emitting = false;
    }
}