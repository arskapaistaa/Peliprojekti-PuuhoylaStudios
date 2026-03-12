using Godot;
using Godot.Collections;
using System;

public partial class QuestionsManager : Node2D
{
	// Int for keeping record.
	public int etsija = 0;
	public int etenija = 0;
	public int edistaja = 0;

	// First question is 0.
	public int question = 0;
	private bool _lastQuestion = false;

	// Can be replaces by using _questionTexts.Lenght()
	[Export] private int _maxQuestionIndex;

	// Choose correct button and animation player in editor.
	[Export] private Button _etsijaButton;
	[Export] private Button _etenijaButton;
	[Export] private Button _edistajaButton;

	// Dialog label
	[Export] private Label _dialogLabel;

	// UI animations
	[Export] private AnimationPlayer _animations;
	// Timer for animations
	[Export] private Timer _animationTimer;

	// String array for question
	[Export] private String[] _questionTexts = {};

	// String array for answer
	[Export] private String[] _etsijaTexts = {};
	[Export] private String[] _etenijaTexts = {};
	[Export] private String[] _edistajaTexts = {};

	public override void _Ready()
	{
		// Connect pressed signalt to method.
		_etsijaButton.Pressed += () => ButtonPressed(1);
		_etenijaButton.Pressed += () => ButtonPressed(2);
		_edistajaButton.Pressed += () => ButtonPressed(3);

		// Show first question.
		NextQuestion();

		// When time reaches 0, call TimerTimeout method.
		_animationTimer.Timeout += TimerTimeout;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame
	public override void _Process(double delta)
	{
	}

	// Method which can be called when button is pressed.
	private void ButtonPressed(int buttonNumber)
	{
		switch (buttonNumber)
		{
			case 1:
			etsija++;
			GD.Print("Etsijä button pressed");
			break;

			case 2:
			etenija++;
			GD.Print("Etenijä button pressed");
			break;

			case 3:
			edistaja++;
			GD.Print("Edistaja button pressed");
			break;
		}

		// After answering question count +1.
		question++;

		if (question == _maxQuestionIndex)
		{
			_lastQuestion = true;
			LastQuestion();
		}
		else
		{
		// Play normal fade animation.
		_animations.Play("FadeOutIn");
		// Start timer.
		_animationTimer.Start();
		}

		// Only for Debuggin
		GD.Print("Etsijä: " + etsija);
		GD.Print("Etenijä: " + etenija);
		GD.Print("Edistjä: " + edistaja);
		GD.Print(question);
	}
	private void NextQuestion()
	{
		// Basic operation
		_dialogLabel.Text = _questionTexts[question];

		_etsijaButton.Text = _etsijaTexts[question];
		_etenijaButton.Text = _etenijaTexts[question];
		_edistajaButton.Text = _edistajaTexts[question];
	}
	private void LastQuestion()
	{
		// Play this if player has answered to all the questions.
		_animations.Play("FadeOut");
		GetTree().ChangeSceneToFile("res://Scenes/Game Scenes/Final_game_scene.tscn");
	}

	// Simple method, what happens after timer timeout. -> Next question.
	public void TimerTimeout()
	{
		NextQuestion();
	}
}
