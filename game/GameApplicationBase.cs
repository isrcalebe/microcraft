global using static Raylib_cs.Raylib;
using System;
using Arch.Core;
using Arch.System;
using microcraft.Game.Systems;
using microcraft.Game.Timing;
using Schedulers;

namespace microcraft.Game;

public abstract class GameApplicationBase : IDisposable
{
    protected World? World { get; private set; }

    protected JobScheduler? Scheduler { get; private set; }

    protected Group<IFrameBasedClock>? Systems { get; private set; }

    protected DrawSystem? DrawSystem { get; private set; }

    protected IFrameBasedClock? Clock { get; private set; }

    public void Initialise()
    {
        InitWindow(GameEnvironment.GAME_WIDTH, GameEnvironment.GAME_HEIGHT, "MICROCRAFT");
        SetTargetFPS(GameEnvironment.GAME_TARGET_FPS);

        if (!IsWindowReady())
            return;

        World = World.Create();
        Scheduler = new JobScheduler(
            new JobScheduler.Config
            {
                ThreadPrefixName = "MICROBLOCKS",
                ThreadCount = 0,
                MaxExpectedConcurrentJobs = 64,
                StrictAllocationMode = false,
            }
        );
        Systems = new Group<IFrameBasedClock>(
            "MICROBLOCKS Systems"
        );
        DrawSystem = new DrawSystem(World);
        Clock = new FramedClock();

        Load();
    }

    protected virtual void Load()
    {
        Systems?.Initialize();
        DrawSystem?.Initialize();

        while (!WindowShouldClose())
        {
            Process();
            Draw();
        }
    }

    protected virtual void Process()
    {
        Systems?.BeforeUpdate(Clock!);
        Systems?.Update(Clock!);
        Systems?.AfterUpdate(Clock!);
    }

    protected virtual void Draw()
    {
        DrawSystem?.BeforeUpdate(Clock!);
        DrawSystem?.Update(Clock!);
        DrawSystem?.AfterUpdate(Clock!);
    }

    #region IDisposable Support

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (disposedValue) return;

        if (disposing)
        {
        }

        if (World != null)
            World.Destroy(World);

        Scheduler?.Dispose();
        Systems?.Dispose();

        CloseWindow();
        disposedValue = true;
    }

    ~GameApplicationBase()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
