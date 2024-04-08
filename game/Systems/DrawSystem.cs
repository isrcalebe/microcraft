using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.System;
using microcraft.Game.Components;
using microcraft.Game.Timing;
using Raylib_cs;
using Silk.NET.Maths;

namespace microcraft.Game.Systems;

public partial class DrawSystem : BaseSystem<World, IFrameBasedClock>
{
    public DrawSystem(World world)
        : base(world)
    {
    }

    public override void BeforeUpdate(in IFrameBasedClock t)
    {
        base.BeforeUpdate(in t);

        BeginDrawing();
    }

    public override void Update(in IFrameBasedClock t)
    {
        base.Update(in t);

        ClearBackground(Color.RayWhite);
    }

    [Query]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Draw(ref TransformComponent transform, ref SpriteComponent sprite)
    {
        TraceLog(TraceLogLevel.Debug, $"{sprite.Colour}");
        DrawTextureEx(sprite.Texture, transform.Position.ToSystem(), transform.Rotation, transform.Scale, sprite.Colour);
    }

    public override void AfterUpdate(in IFrameBasedClock t)
    {
        base.AfterUpdate(in t);

        EndDrawing();
    }
}
