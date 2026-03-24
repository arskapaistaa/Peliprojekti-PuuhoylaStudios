using Godot;
using System;

public partial class Mouse : CharacterBody2D
{
    [Export] public float speed = 200f;
    [Export] private CpuParticles2D _particles;

    // For animations
    [Export] private AnimatedSprite2D _sprite;
    public override void _PhysicsProcess(double delta)
    {
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

            // Start emiting when moving.
            _particles.Emitting = true;
        }
        else
        {
            // Play idle when Idle state
            _sprite.Play("Idle");

            // Sttop emiting when Idle state.
            _particles.Emitting = false;
        }
    }
}