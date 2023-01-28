using Godot;
using System;

public class Danger : RigidBody2D
{
    [Export]
    public float rotationSpeed;
    public Sprite sprite;
    RandomNumberGenerator rnd = new RandomNumberGenerator();

    public override void _Ready()
    {
        //Randomize sprite danger
        rnd.Randomize();

        sprite = GetNode<Sprite>("Sprite");
        Vector2 textureLocation = new Vector2(rnd.RandiRange(0, 3) * 32, 0);
        sprite.RegionRect = new Rect2(textureLocation, new Vector2(32, 32));
        sprite.Modulate = new Color(sprite.Modulate.r, sprite.Modulate.g, sprite.Modulate.b, 0);

        base._Ready();
    }

    public async void ShowDanger()
    {
        for (float i = 0; i <= 1; i += 0.2f)
        {
            sprite.Modulate = new Color(sprite.Modulate.r, sprite.Modulate.g, sprite.Modulate.b, i);
            await ToSignal(GetTree().CreateTimer(0.05f), "timeout");
        }
    }

    public async void Destroy()
    {
        //LinearVelocity = new Vector2();
        GetNode<CollisionShape2D>("Collusion").SetDeferred("disabled", true);
        GetNode<Particles2D>("DestroyingEffect").Emitting = true;
        sprite.Modulate = new Color(sprite.Modulate.r, sprite.Modulate.g, sprite.Modulate.b, 0);
        await ToSignal(GetTree().CreateTimer(2), "timeout");
        QueueFree();
    }

    public override void _Process(float delta)
    {
        sprite.RotationDegrees += rotationSpeed * delta;
        base._Process(delta);
    }

}
