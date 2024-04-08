using System;
using microcraft.Game.Components;
using Raylib_cs;
using Silk.NET.Maths;

namespace microcraft.Game;

public class GameApplication : GameApplicationBase
{
    private Tuple<Image, Texture2D>[] disposableItems = new Tuple<Image, Texture2D>[10];

    private static (Image, Texture2D) createSquareTexture(int size)
    {
        var image = GenImageColor(size, size, getRandomColor());
        var texture = LoadTextureFromImage(image);

        return (image, texture);
    }

    private static Color getRandomColor()
        => new Color(
            Random.Shared.Next(1, 255),
            Random.Shared.Next(1, 255),
            Random.Shared.Next(1, 255),
            255
        );

    private static Vector2D<float> getRandomVector(float min, float max)
    {
        return new Vector2D<float>(
            Random.Shared.NextSingle() * (min - max) + min,
            Random.Shared.NextSingle() * (min - max) + min
        );
    }

    protected override void Load()
    {
        for (var i = 0; i < 10; i++)
        {
            var (image, texture)  = createSquareTexture(100);
            disposableItems[i] = new Tuple<Image, Texture2D>(image, texture);

            World?.Create(
                new TransformComponent
                {
                    Position = getRandomVector(-500.0f, 5000.0f),
                },
                new SpriteComponent
                {
                    Texture = texture,
                    Colour = getRandomColor()
                }
            );
        }

        base.Load();
    }

    protected override void Dispose(bool disposing)
    {
        foreach (var (image, texture) in disposableItems)
        {
            UnloadImage(image);
            UnloadTexture(texture);
        }

        base.Dispose(disposing);
    }
}
