using System;
using System.Numerics;
using Raylib_cs;

namespace microcraft.Game;

public class GameApplication : GameApplicationBase
{
    public override void Process()
    {
        Console.WriteLine(Clock);

        base.Process();
    }
}
