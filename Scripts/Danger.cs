using Godot;
using System;

public class Danger : RigidBody2D
{
    [Export]
    public float rotationSpeed;
    RandomNumberGenerator rnd = new RandomNumberGenerator();

    public override void _Ready()
    {
        //Randomize sprite danger
        rnd.Randomize();
        Vector2 textureLocation = new Vector2(rnd.RandiRange(0, 3) * 32, 0);
        GetNode<Sprite>("Sprite").RegionRect = new Rect2(textureLocation, new Vector2(32, 32));

        base._Ready();
    }

    public override void _Process(float delta)
    {
        GetNode<Sprite>("Sprite").RotationDegrees += rotationSpeed * delta;
        base._Process(delta);
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }

}
