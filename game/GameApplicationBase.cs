global using static Raylib_cs.Raylib;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Arch.Core;
using Arch.System;
using microcraft.Game.IO.Providers;
using microcraft.Game.Resources;
using microcraft.Game.Systems;
using Raylib_cs;
using Schedulers;
using Serilog;
using Serilog.Core;

namespace microcraft.Game;

public abstract class GameApplicationBase : IDisposable
{
    protected World? World { get; private set; }

    protected JobScheduler? Scheduler { get; private set; }

    protected Group<float>? Systems { get; private set; }

    protected DrawSystem? DrawSystem { get; private set; }

    protected Logger? Logger { get; private set; }

    protected AssetProvider<byte[]>? Assets { get; private set; }

    public void Initialise()
    {
        setupLogging();
        setupAssets();
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
        Systems = new Group<float>(
            "MICROBLOCKS Systems"
        );
        DrawSystem = new DrawSystem(World, new Camera2D
        {
            Zoom = 1.0f,
            Rotation = 0.0f,
            Target = Vector2.Zero,
            Offset = new Vector2(
                GameEnvironment.RENDERER_WIDTH / 2.0f,
                GameEnvironment.RENDERER_HEIGHT / 2.0f)
        });

        Load();
    }

    protected virtual void Load()
    {
        Systems?.Initialize();
        DrawSystem?.Initialize();

        while (!WindowShouldClose())
        {
            var elapsed = GetFrameTime();

            Process(elapsed);
            Draw(elapsed);
        }
    }

    protected virtual void Process(float elapsedFrameTime)
    {
        Systems?.BeforeUpdate(in elapsedFrameTime);
        Systems?.Update(in elapsedFrameTime);
        Systems?.AfterUpdate(in elapsedFrameTime);
    }

    protected virtual void Draw(float elapsedFrameTime)
    {
        DrawSystem?.BeforeUpdate(in elapsedFrameTime);
        DrawSystem?.Update(in elapsedFrameTime);
        DrawSystem?.AfterUpdate(in elapsedFrameTime);
    }

    private void setupAssets()
    {
        Assets = new AssetProvider<byte[]>();
        Assets.AddNestedProvider(new AssemblyAssetProvider(GameAssetAssemblyProvider.Assembly));
        Assets.AddExtension(".png");
    }

    private void setupLogging()
    {
        var loggerConfiguration = new LoggerConfiguration()
                                  .WriteTo.Console();

        Logger = loggerConfiguration.CreateLogger();
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = true)]
    private delegate void RaylibTracelogCallback(int level, string message, ArgIterator args);

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
        DrawSystem?.Dispose();

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
