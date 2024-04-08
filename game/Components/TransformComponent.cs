using Silk.NET.Maths;

namespace microcraft.Game.Components;

public struct TransformComponent
{
    private Vector2D<float> position;

    public Vector2D<float> Position
    {
        get => position;
        set => position = value;
    }

    public float X
    {
        get => position.X;
        set => position.X = value;
    }

    public float Y
    {
        get => position.Y;
        set => position.Y = value;
    }

    public float Rotation { get; set; }

    public float Scale { get; set; } = 1.0f;

    public TransformComponent()
    {
    }
}
