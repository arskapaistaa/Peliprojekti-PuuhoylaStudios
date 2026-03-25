using Godot;
using System;
using System.ComponentModel;

public partial class Muskrat : Area2D
{
	[Export] private int _hireCost;
	[Export] private AnimationPlayer _animations;
	[Export] private Label _dialogueLabel;
	[Export] private Button _hireButton;
	private bool _isHired = false;
	private bool wasJustHired = false;
	public override void _Ready()
	{
		_hireButton.Pressed += OnHirePressed;
	}
	private void OnHirePressed()
	{
		GD.Print("Hire button pressed");
		if (GameManager.Instance.CheeseScore >= _hireCost)
		{
			GameManager.Instance.RemoveCheese(_hireCost);
			_dialogueLabel.Text = "Thank you for the cheese! I'll help you get out of here.";
			_hireButton.Visible = false;
			_isHired = true;
			wasJustHired = true;
		}
	}
	public override void _EnterTree()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExit;
	}
	public override void _ExitTree()
	{
		BodyEntered -= OnBodyEntered;
		BodyExited -= OnBodyExit;
	}

	// Method when player enters the area.
	private void OnBodyEntered(Node2D body)
	{
		if (body is Mouse && !_isHired)
		{
			GD.Print("Player Entered");
			if (GameManager.Instance.CheeseScore >= _hireCost)
			{
				_dialogueLabel.Text = " If you give me some cheese,\nI can help you get out of here.";
				_hireButton.Visible = true;
			}
			else
			{
				_dialogueLabel.Text = $"You need at least {_hireCost} cheese to hire me.";
				_hireButton.Visible = false;
			}
			_animations.Play("PopUp");
		}
	}

	// Method when player exits the area.
	private void OnBodyExit(Node2D body)
	{
		if (body is Mouse && (!_isHired || wasJustHired))
		{
			GD.Print("Player Exit");
			_animations.Play("PopOut");
			if (wasJustHired)
			{
				wasJustHired = false;
			}
		}
	}
}
