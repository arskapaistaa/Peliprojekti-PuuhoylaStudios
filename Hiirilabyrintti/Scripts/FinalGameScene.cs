using Godot;
using System;

public partial class FinalGameScene : Control
{
	[Export] private Label _firstDialog;

	// Arrays are for lanquages.
	[Export] private String[] _firstDialogArray = {};
	[Export] private String[] _edistaja = {};
	[Export] private String[] _etenija = {};
	[Export] private String[] _etsija = {};

	[Export] private ColorRect _edistajaBar;
	[Export] private ColorRect _etenijaBar;
	[Export] private ColorRect _estijaBar;
	[Export] private Label _edistajaLabel;
	[Export] private Label _etenijaLabel;
	[Export] private Label _etsijaLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Set First dialog Fin or Eng
		_firstDialog.Text = _firstDialogArray[SettingsManager.Instance.Language];

		// Set color rect sizes to Score * 8. Beacause max width is 800px, and we have currently 10 questions.

		_edistajaBar.SetSize(new Vector2(GameManager.Instance.EdistajaScore * 8, 40));
		_etenijaBar.SetSize(new Vector2(GameManager.Instance.EtenijaScore * 8, 40));
		_estijaBar.SetSize(new Vector2(GameManager.Instance.EtsijaScore * 8, 40));

		// Set label texts, add Score and %.
		_edistajaLabel.Text = _edistaja[SettingsManager.Instance.Language] + " " + GameManager.Instance.EdistajaScore + "%";
		_etenijaLabel.Text = _etenija[SettingsManager.Instance.Language] + " " + GameManager.Instance.EtenijaScore + "%";
		_etsijaLabel.Text = _etsija[SettingsManager.Instance.Language] + " " + GameManager.Instance.EtsijaScore + "%";

	}
}
