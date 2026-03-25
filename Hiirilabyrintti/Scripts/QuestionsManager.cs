using Godot;
using Godot.Collections;
using System;

public partial class QuestionsManager : Node2D
{

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

		_animations.AnimationFinished += OnAnimationFinished;

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
			GameManager.Instance.AddEtsija(10);
			break;

			case 2:
			GameManager.Instance.AddEtenija(10);
			break;

			case 3:
			GameManager.Instance.AddEdistaja(10);
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

		// Disable all buttons.
		ButtonsDisabled(true);

		// Start timer.
		_animationTimer.Start();
		}

		// Debugging
		GD.Print("Question: " + question);
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

		// Load new scene.
		GetTree().ChangeSceneToFile("res://Scenes/Game Scenes/Final_game_scene.tscn");
	}

	// Simple method, what happens after timer timeout. -> Next question.
	public void TimerTimeout()
	{
		NextQuestion();
	}

	public void ButtonsDisabled(bool isDisabled)
	{
		// Disable buttons when true -> After answering to a question.
		if(isDisabled == true)
		{
			_etsijaButton.Disabled = true;
			_etenijaButton.Disabled = true;
			_edistajaButton.Disabled = true;

			GD.Print("Buttons disabled");
		}
		else if (isDisabled == false)
		{
			_etsijaButton.Disabled = false;
			_etenijaButton.Disabled = false;
			_edistajaButton.Disabled = false;

			GD.Print("Buttons enabled");
		}
	}
	private void OnAnimationFinished(StringName animationName)
	{
		if (animationName == "FadeOutIn")
		{
			ButtonsDisabled(false);
		}
	}
}
