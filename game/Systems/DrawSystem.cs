using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.System;
using microcraft.Game.Components;
using Raylib_cs;
using Silk.NET.Maths;

namespace microcraft.Game.Systems;

public partial class DrawSystem : BaseSystem<World, float>
{
    public DrawSystem(World world)
        : base(world)
    {
    }

    public override void BeforeUpdate(in float t)
    {
        base.BeforeUpdate(in t);

        BeginDrawing();
    }

    public override void Update(in float t)
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

    public override void AfterUpdate(in float t)
    {
        base.AfterUpdate(in t);

        EndDrawing();
    }
}
