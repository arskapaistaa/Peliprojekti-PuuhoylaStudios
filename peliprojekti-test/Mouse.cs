using Godot;
using System;

public partial class Mouse : CharacterBody2D
{
    [Export] public float speed = 200f;
    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = new Vector2(
            Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
            Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up"));

        inputDirection = inputDirection.Normalized();
        Velocity = inputDirection * speed;
        MoveAndSlide();
    }
}