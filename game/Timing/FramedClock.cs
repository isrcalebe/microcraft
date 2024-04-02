using System;
using System.Globalization;
using microcraft.Game.Timing;

namespace microcraft.Game;

public sealed class FramedClock : IFrameBasedClock
{
    public double ElapsedFrameTime => GetFrameTime();

    public double FramesPerSecond => GetFPS();

    public bool IsRunning => !WindowShouldClose();

    public double CurrentTime => GetTime();

    public override string ToString()
        => $"({ElapsedFrameTime}, {FramesPerSecond})";
}
