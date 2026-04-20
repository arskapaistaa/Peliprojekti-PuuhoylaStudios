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
	[Export] private AudioStreamPlayer _buttonDownSFX = null;
	[Export] public GpuParticles2D drillParticles;

	private Rock _rock;

	public override void _Ready()
	{
		_drillSprite.Visible = false;
		_hireButton.Pressed += OnHirePressed;

		var area = GetNode<Area2D>("Area2D");
		area.BodyEntered += OnBodyEntered;
		area.BodyExited += OnBodyExit;

		_rock = GetTree().Root.GetNode<Rock>("/root/World/Rock/Area2D");
		_rock.RockDestroyed += OnRockDestroyed;
	}

	private void OnRockDestroyed()
	{

		_drillSprite.Visible = false;
		_drillSprite.Stop();

		drillParticles.Emitting = false;
	}

	private void OnHirePressed()
	{
		GD.Print("Hire button pressed");

		if (GameManager.Instance.CheeseScore >= _hireCost)
		{
			if (SettingsManager.Instance.Language == 0)
			{
				_dialogueLabel.Text = "Kiitoksia juustosta!\nSeuraa minua..";
			}
			else
			{
				_dialogueLabel.Text = "Thank you for the cheese!\nI'll help you get out of here.";
			}

			GameManager.Instance.RemoveCheese(_hireCost);

			_hireButton.Visible = false;

			_isHired = true;
			wasJustHired = true;

			PathFollower pathFollower = GetNode<PathFollower>("/root/World/PathFollower");
			pathFollower._canMove = true;
		}

		if (SettingsManager.Instance.Volume)
		{
			_buttonDownSFX.Play();
		}

		Input.VibrateHandheld(500);
	}

	// Method when player enters the area.
	private void OnBodyEntered(Node2D body)
	{
		if (body is Mouse && !_isHired)
		{
			GD.Print("Player Entered");
			_animations.Play("PopUp");

			if (GameManager.Instance.CheeseScore >= _hireCost)
			{
				if (SettingsManager.Instance.Language == 0) {
					_dialogueLabel.Text = " Jos annat mulle vähä juustoa,\nniin autan sut pihalle täältä.";
					_hireButton.Text = "Palkkaa";
					_hireButton.Visible = true;
				}
				else
				{
					_dialogueLabel.Text = " If you give me some cheese,\nI can help you get out of here.";
					_hireButton.Text = "Hire";
					_hireButton.Visible = true;
				}

			}
			else
			{
				if (SettingsManager.Instance.Language == 0)
				{
					_dialogueLabel.Text = $"Tarvitset vähintään\n{_hireCost} juustoa palkataksesi minut.";
					_hireButton.Visible = false;
				}
				else
				{
					_dialogueLabel.Text = $"You need at least\n{_hireCost} cheese to hire me.";
					_hireButton.Visible = false;
				}
			}
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