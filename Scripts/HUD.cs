using Godot;
using System;

public class HUD : Control
{
    [Export]
    private Vector2[] monitorRect = new Vector2[2];
    public Label message;
    public override void _Ready()
    {
        message = GetNode<Label>("MessageLabel");
        message.Modulate = new Color(message.Modulate.r, message.Modulate.g, message.Modulate.b, 0);
        base._Ready();
    }

    public override void _Process(float delta)
    {
        ResizeMonitor(GetNode<TileMap>("WawesMonitor"), 0);
        base._Process(delta);
    }

    public void ResizeMonitor(TileMap monitor, int monitorIndex, int x = 0, int y = 0)
    {
        Vector2 centreSizeScreen = GetViewportRect().Size / 2;

        switch (monitorIndex)
        {
            case 0:
                monitor.Position = new Vector2(
                    centreSizeScreen.x < monitorRect[monitorIndex].x ? monitorRect[monitorIndex].x - centreSizeScreen.x : centreSizeScreen.x - monitorRect[monitorIndex].x,
                    //sizeScreen.y < monitorRect[monitorIndex].y ? monitorRect[monitorIndex].y - sizeScreen.y : sizeScreen.y - monitorRect[monitorIndex].y
                    monitor.Position.y
                );
                break;

            case 1:
                break;
        }

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

        for (float i = 0; i <= 1; i += 0.2f)
        {
            message.Modulate = new Color(message.Modulate.r, message.Modulate.g, message.Modulate.b, i);
            await ToSignal(GetTree().CreateTimer(0.05f), "timeout");
        }

        await ToSignal(GetTree().CreateTimer(time), "timeout");

        for (float i = 1; i >= 0; i -= 0.2f)
        {
            message.Modulate = new Color(message.Modulate.r, message.Modulate.g, message.Modulate.b, i);
            await ToSignal(GetTree().CreateTimer(0.05f), "timeout");
        }
    }

}
