using microcraft.Game.Components;
using Raylib_cs;

namespace microcraft.Game;

public class GameApplication : GameApplicationBase
{
    private Image image;
    private Texture2D texture;

    protected override void Load()
    {
        var tiles = Assets?.Fetch("Textures/tiles.png");
        image = LoadImageFromMemory(".png", tiles);
        texture = LoadTextureFromImage(image);

        World?.Create(
            new TransformComponent
            {
                X = 256,
                Y = 256,
                Scale = 2.0f,
            },
            new SpriteComponent()
            {
                Texture = texture,
                Colour = Color.White
            });

        base.Load();
    }

    protected override void Dispose(bool disposing)
    {
        UnloadTexture(texture);
        UnloadImage(image);

        base.Dispose(disposing);
    }
}
