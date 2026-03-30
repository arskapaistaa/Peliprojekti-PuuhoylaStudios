using Godot;
using System;

public partial class Mouse : CharacterBody2D
{
    [Export] public float speed = 160f;
    [Export] private float _slowedSpeed = 50.0f;
    [Export] private GpuParticles2D _walkParticles;

    // For animations
    [Export] private AnimatedSprite2D _sprite;

    [Export] private CameraShake _camera;
    private bool wasOnWall = false;
    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = new Vector2(
            Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
            Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up"));

        // Add this line if wanted:
        // inputDirection = inputDirection.Normalized();

        Velocity = inputDirection * speed;
        MoveAndSlide();

        if (Velocity.Length() > 0)
        {
            Rotation = Velocity.Angle() + Mathf.Pi / 2f;

            // Play Move when moving
            _sprite.Play("Move");

            // Start emiting when moving.
            _walkParticles.Emitting = true;
        }
        else
        {
            // Play idle when Idle state
            _sprite.Play("Idle");

            // Sttop emiting when Idle state.
            _walkParticles.Emitting = false;
        }

        // Camera shake when colliding with wall.
        // Only on impact.
        bool isOnWall = IsOnWall();
        if (isOnWall && !wasOnWall)
        {
            _camera.Shake(2.0f);

            // Also vibration 100ms.
            Input.VibrateHandheld(100);
        }
        wasOnWall = isOnWall;

        if (IsOnWall())
        {
            speed = _slowedSpeed;
        }
        else if (!IsOnWall())
        {
            //Check from editor!!
            speed = 160.0f;
        }
    }
}