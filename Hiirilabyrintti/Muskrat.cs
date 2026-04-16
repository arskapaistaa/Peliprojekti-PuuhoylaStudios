using Godot;
using System;

public partial class Muskrat : CharacterBody2D
{
	[Export] private int _hireCost;
	[Export] private AnimationPlayer _animations;
	[Export] private Label _dialogueLabel;
	[Export] private Button _hireButton;
	[Export] public AnimatedSprite2D _drillSprite;
	public bool _isHired = false;
	private bool wasJustHired = false;


	private Rock _rock;

	public override void _Ready()
	{
		_drillSprite.Visible = false;
		_hireButton.Pressed += OnHirePressed;

		var area = GetNode<Area2D>("Area2D");
		area.BodyEntered += OnBodyEntered;
		area.BodyExited += OnBodyExit;

		_rock = GetTree().Root.GetNode<Rock>("/root/World/Maze/Rock/Area2D");
		_rock.RockDestroyed += OnRockDestroyed;
	}

	private void OnRockDestroyed()
	{

		_drillSprite.Visible = false;
		_drillSprite.Stop();
	}

	private void OnHirePressed()
	{
		GD.Print("Hire button pressed");

		if (GameManager.Instance.CheeseScore >= _hireCost)
		{
			GameManager.Instance.RemoveCheese(_hireCost);
			_dialogueLabel.Text = "Thank you for the cheese!\nI'll help you get out of here.";
			_hireButton.Visible = false;

			_isHired = true;
			wasJustHired = true;

			PathFollower pathFollower = GetNode<PathFollower>("/root/World/PathFollower");
			pathFollower._canMove = true;
		}
	}

	// Method when player enters the area.
	private void OnBodyEntered(Node2D body)
	{
		if (body is Mouse && !_isHired)
		{
			GD.Print("Player Entered");

			if (GameManager.Instance.CheeseScore >= _hireCost)
			{
				if (SettingsManager.Instance.Language == 0) {
					_dialogueLabel.Text = " Jos annat mulle vähä juusota,\nniin autan sut pihalle täältä.";
					_hireButton.Visible = true;
				}
				else
				{
					_dialogueLabel.Text = " If you give me some cheese,\nI can help you get out of here.";
					_hireButton.Visible = true;
				}

			}
			else
			{
				if (SettingsManager.Instance.Language == 0)
				{
					_dialogueLabel.Text = $"Tarvitset vähintään\n{_hireCost} juustoa palkataksesi minut.";

					_hireButton.Text = "Palkkaa";
					_hireButton.Visible = false;
				}
				else
				{
					_dialogueLabel.Text = $"You need at least\n{_hireCost} cheese to hire me.";
					_hireButton.Text = "Hire";
					_hireButton.Visible = false;
				}
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