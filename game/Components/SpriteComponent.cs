using Raylib_cs;

namespace microcraft.Game.Components;

public struct SpriteComponent
{
    public Texture2D Texture { get; set; }

    public Color Colour { get; set; }

    public SpriteComponent(Texture2D texture, Color colour)
    {
        Texture = texture;
        Colour = colour;
    }
}
