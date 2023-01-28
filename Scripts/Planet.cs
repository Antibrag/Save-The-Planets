using Godot;
using System.Collections;

public class Planet : Area2D
{
    [Export]
    public float planetRotationSpeed = 100;

    RandomNumberGenerator rnd = new RandomNumberGenerator();

    public void ResizePlanet()
    {
        Vector2 sizeScreen = GetViewportRect().Size;
        Position = sizeScreen / 2;
    }

    public override void _Process(float delta)
    {
        GetNode<Sprite>("PlanetSprite").RotationDegrees += planetRotationSpeed * delta;
        base._Process(delta);
        ResizePlanet();
    }

    public void GeneratePlanet()
    {
        rnd.Randomize();
        Vector2 textureLocation = new Vector2(rnd.RandiRange(0, 3) * 32, rnd.RandiRange(0, 3) * 32);
        GetNode<Sprite>("PlanetSprite").RegionRect = new Rect2(textureLocation, new Vector2(32, 32));
    }
}