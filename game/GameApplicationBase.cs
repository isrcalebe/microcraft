global using static Raylib_cs.Raylib;
using System;
using microcraft.Game.Timing;

namespace microcraft.Game;

public abstract class GameApplicationBase : IDisposable
{
    public IFrameBasedClock Clock = new FramedClock();

    public void Initialise()
    {
        InitWindow(GameEnvironment.GAME_WIDTH, GameEnvironment.GAME_HEIGHT, "MICROCRAFT");
        SetTargetFPS(GameEnvironment.GAME_TARGET_FPS);

        Load();

        while (!WindowShouldClose())
        {
            Process();

            BeginDrawing();
            Draw();
            EndDrawing();
        }
    }

    public virtual void Load()
    {
    }

    public virtual void Process()
    {
    }

    public virtual void Draw()
    {
    }


    #region IDisposable Support

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
            }

            CloseWindow();
            disposedValue = true;
        }
    }

    ~GameApplicationBase()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
