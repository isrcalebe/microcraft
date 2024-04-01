using System.Numerics;
using Raylib_cs;

namespace microcraft.Game;

public class GameApplication : GameApplicationBase
{
    private string text = "Hello, world!";
    private Vector2 textSize, textPosition;


    public override void Load()
    {
        textSize = MeasureTextEx(GetFontDefault(), text, 64, 0);
        textPosition = new Vector2((GameEnvironment.GAME_WIDTH - textSize.X) / 2, (GameEnvironment.GAME_HEIGHT - textSize.Y) / 2);

        base.Load();
    }

    public override void Draw()
    {
        ClearBackground(Color.RayWhite);
        DrawText(text, (int)textPosition.X, (int)textPosition.Y, 64, Color.LightGray);

        base.Draw();
    }
}
