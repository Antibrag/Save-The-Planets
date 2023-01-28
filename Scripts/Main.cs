using Godot;
using System;

public class Main : Node
{
    [Export]
    public PackedScene DanScene;
    [Export]
    public float dangerSpeed;
    private const float KF_GANG = 0.5f;
    Data dt = new Data();

    public override void _Ready()
    {
        //System configured
        GD.Randomize();
        dt.Initialize();
        dt.LoadData();

        //Game activities
        GetNode<Timer>("BetweenWawesTimer").Start();
        base._Ready();
    }

    public override void _Process(float delta)
    {
        GetNode<HUD>("HUD").ShowGameStats(Data.money, Data.wawe,
        (float)Math.Round(GetNode<Timer>("WawesTimer").TimeLeft, 1),
        (float)Math.Round(GetNode<Timer>("BetweenWawesTimer").TimeLeft, 1));
        base._Process(delta);
    }


    public void WawesEnd()
    {
        GetNode<Timer>("DangerSpawnTimer").Stop();
        GetNode<Timer>("BetweenWawesTimer").Start();
        GetNode<Label>("HUD/BetweenWaweTimerLabel").Show();
        GetNode<HUD>("HUD").ShowMessage("Волна закончилась!", 1, GetNode<Label>("HUD/MessageLabel"));
        Data.wawe++;

        dt.SaveData();
    }

    public void OnPlanetBodyEntered(Danger body)
    {
        body.Destroy();
        GetNode<Timer>("DangerSpawnTimer").Stop();
        GetNode<Timer>("WawesTimer").Stop();
        GetNode<Timer>("BetweenWawesTimer").Start();
        GetNode<Label>("HUD/BetweenWaweTimerLabel").Show();
        GetNode<HUD>("HUD").ShowMessage("Поражение!", 1, GetNode<Label>("HUD/MessageLabel"));
        dt.SaveData();
    }

    public void OnBetweenWawesTimerTimeout()
    {
        GetNode<Timer>("WawesTimer").WaitTime = 10 + (Data.wawe * KF_GANG);
        GetNode<HUD>("HUD").ShowMessage("Волна началась!", 1, GetNode<Label>("HUD/MessageLabel"));
        GetNode<Timer>("WawesTimer").Start();
        GetNode<Timer>("DangerSpawnTimer").Start();
        GetNode<Label>("HUD/BetweenWaweTimerLabel").Hide();
    }

    public void OnDangerSpawnTimerTimeout() { SpawnDangers(); }

    public void SpawnDangers()
    {
        var dang = (Danger)DanScene.Instance();
        var dangerSpawnLocation = GetNode<PathFollow2D>("SpawnDanger/Spawner");
        dangerSpawnLocation.Offset = GD.Randi();

        dang.Position = dangerSpawnLocation.Position;

        AddChild(dang);

        dang.LookAt(GetNode<Area2D>("Planet").Position);
        dang.LinearVelocity = new Vector2(dangerSpeed + (Data.wawe * KF_GANG), 0).Rotated(dang.Rotation);
        dang.ShowDanger();
    }

}