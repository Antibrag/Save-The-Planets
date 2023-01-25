using Godot;
using System;

public class HUD : Control
{

    public override void _Ready()
    {
        GetNode<Label>("MessageLabel").Hide();
        base._Ready();
    }

    public void ShowGameStats(int score, int wawe, float time_left = 0, float time_between_wawe = 0)
    {
        GetNode<Label>("MoneyLabel").Text = $"Монет: {score}";
        GetNode<Label>("WawesNumberLabel").Text = $"Волна: {wawe}";
        GetNode<Label>("WawesTimerLabel").Text = $"Время волны: {time_left}";
        GetNode<Label>("BetweenWaweTimerLabel").Text = $"Время до волны: {time_between_wawe}";
    }

    public async void ShowMessage(string text, float time, Label label)
    {
        label.Text = text;
        label.Show();
        await ToSignal(GetTree().CreateTimer(time), "timeout");
        label.Hide();
    }

}
