namespace microcraft.Game.Timing;

public interface IFrameBasedClock : IClock
{
    double ElapsedFrameTime { get; }

    double FramesPerSecond { get; }
}
