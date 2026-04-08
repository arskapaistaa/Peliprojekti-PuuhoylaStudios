using Godot;
using System;

public partial class Mouse : CharacterBody2D
{
    [Export] public float speed = 200f;

    // For animations
    [Export] private AnimatedSprite2D _sprite;
    [Export] private Control DrillPrompt;
    [Export] private Button YesButton;
    [Export] private Button NoButton;
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
    }
    public override void _PhysicsProcess(double delta)
    {
        if (!GameManager.Instance.MouseCanMove)
        {
            speed = 0f;
        } else
        {
            speed = 200f;
        }

        Vector2 inputDirection = new Vector2(
            Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
            Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up"));

        inputDirection = inputDirection.Normalized();
        Velocity = inputDirection * speed;
        MoveAndSlide();

        if (Velocity.Length() > 0)
        {
            Rotation = Velocity.Angle() + Mathf.Pi / 2f;

            // Play Move when moving
            _sprite.Play("Move");
        }
        else
        {
            // Play idle when Idle state
            _sprite.Play("Idle");
        }
    }

    private void ShowDrillPrompt()
    {
        GameManager.Instance.MouseCanMove = false;
        DrillPrompt.Visible = true;
    }
    private void OnYesPressed()
    {
        GD.Print("You chose to build the drill!");

        GameManager.Instance.RemoveDrill(neededToBuildDrill);
        DrillPrompt.Visible = false;
        GameManager.Instance.MouseCanMove = true;
        GameManager.Instance._hasDrill = true;
        GameManager.Instance.counters.BuiltDrill.Visible = true;
    }
    private void OnNoPressed()
    {
        GD.Print("You chose not to build the drill.");

        DrillPrompt.Visible = false;
        GameManager.Instance.MouseCanMove = true;
    }
}