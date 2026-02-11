using Godot;
using System;

public partial class CheeseCounter : CanvasLayer
{
    [Export] public Label counterLabel;
    private int collectedCount = 0;

    public void AddCollectible()
    {
        collectedCount++;
        counterLabel.Text = collectedCount.ToString();
    }
}
