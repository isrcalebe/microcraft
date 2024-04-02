namespace microcraft.Game.Timing;

public interface IClock
{
    bool IsRunning { get; }

    double CurrentTime { get; }
}
