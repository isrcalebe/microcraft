using System;
using System.IO;
using System.Reflection;
using microcraft.Game.Extensions.ObjectExtensions;

namespace microcraft.Game;

public static class GameEnvironment
{
    public static readonly int GAME_WIDTH;
    public static readonly int GAME_HEIGHT;

    public static readonly int RENDERER_WIDTH;
    public static readonly int RENDERER_HEIGHT;

    public static readonly int GAME_TARGET_FPS;

    public static readonly string GAME_ASSEMBLY_LOCATION;

    static GameEnvironment()
    {
        GAME_WIDTH = getEnvVar(nameof(GAME_WIDTH), 1366);
        GAME_HEIGHT = getEnvVar(nameof(GAME_HEIGHT), 768);

        RENDERER_WIDTH = getEnvVar(nameof(RENDERER_WIDTH), 256);
        RENDERER_HEIGHT = getEnvVar(nameof(RENDERER_HEIGHT), 144);

        GAME_TARGET_FPS = getEnvVar(nameof(GAME_TARGET_FPS), 60);

        GAME_ASSEMBLY_LOCATION = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).AsNonNull();
    }

    private static T getEnvVar<T>(string name, T defaultValue = default)
        where T : struct
    {
        var value = Environment.GetEnvironmentVariable(name);
        if (value != null)
            return (T)Convert.ChangeType(value, typeof(T));

        return defaultValue;
    }
}
