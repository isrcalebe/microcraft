using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using microcraft.Game.IO.Providers;

namespace microcraft.Game.Graphics.Textures;

public class TextureProvider : IAssetProvider<Texture>
{
    private readonly AssetProvider<byte[]> provider;

    public IEnumerable<string> AvailableAssets
        => provider.AvailableAssets
                   .Where(x => x.EndsWith(".png", StringComparison.Ordinal));

    public TextureProvider(IAssetProvider<byte[]> provider)
    {
        this.provider = new AssetProvider<byte[]>(provider);
        this.provider.AddExtension(@"png");
    }

    public Texture? Fetch(string name)
    {
        var bytes = provider.Fetch(name);
        var image = LoadImageFromMemory(".png", bytes);
        var rayTexture = LoadTextureFromImage(image);

        UnloadImage(image);

        return new Texture(rayTexture);
    }

    public async Task<Texture?> FetchAsync(string name, CancellationToken cancellationToken = default)
    {
        var bytes = await provider.FetchAsync(name, cancellationToken).ConfigureAwait(false);

        if (bytes == null) return default;

        var image = LoadImageFromMemory(".png", bytes);
        var rayTexture = LoadTextureFromImage(image);

        UnloadImage(image);

        return new Texture(rayTexture);
    }

    public Stream? GetStream(string name)
        => provider.GetStream(name);

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        provider.Dispose();
    }
}
