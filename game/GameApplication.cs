using microcraft.Game.Components;
using Raylib_cs;

namespace microcraft.Game;

public class GameApplication : GameApplicationBase
{
    protected override void Load()
    {
        using var texture = Textures.Fetch("tiles.png");

        World?.Create(
            new TransformComponent
            {
                X = 256,
                Y = 256,
                Scale = 2.0f,
                Rotation = 45.0f,
            },
            new SpriteComponent()
            {
                Texture = texture,
                Colour = Color.White
            });

        base.Load();
    }
}
