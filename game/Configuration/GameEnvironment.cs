using System;

namespace microcraft.Game;

public static class GameEnvironment
{
    public static readonly int GAME_WIDTH;
    public static readonly int GAME_HEIGHT;

    public static readonly int GAME_TARGET_FPS;

    static GameEnvironment()
    {
        GAME_WIDTH = getEnvVar(nameof(GAME_WIDTH), 1366);
        GAME_HEIGHT = getEnvVar(nameof(GAME_HEIGHT), 768);

        GAME_TARGET_FPS = getEnvVar(nameof(GAME_TARGET_FPS), 60);
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
