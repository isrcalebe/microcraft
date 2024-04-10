using System;
using microcraft.Game.Extensions.ObjectExtensions;
using Raylib_cs;
using Silk.NET.Maths;

namespace microcraft.Game.Graphics.Textures;

public sealed class Texture : IDisposable
{
    public Texture2D Handle { get; private set; }

    public Vector2D<int> Size => new(Handle.Width, Handle.Height);

    public int Width => Size.X;

    public int Height => Size.Y;

    public Texture(Texture2D handle)
    {
        Handle = handle;
    }

    public static implicit operator Texture2D(Texture texture) => texture.Handle;

    #region IDisposable Support

    private void releaseUnmanagedResources()
    {
        if (Handle.IsNotNull())
            UnloadTexture(Handle);
    }

    public void Dispose()
    {
        releaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Texture()
    {
        releaseUnmanagedResources();
    }

    #endregion
}
