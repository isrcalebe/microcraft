using System;
using System.Collections.Generic;
using System.Linq;

namespace microcraft.Game.IO.Providers;

public class NamespacedAssetProvider<T> : AssetProvider<T>
    where T : class
{
    public string Namespace;

    public override IEnumerable<string> AvailableAssets
        => base.AvailableAssets
               .Where(source => source.StartsWith($"{Namespace}/", StringComparison.Ordinal))
               .Select(source => source[(Namespace.Length + 1)..]);

    public NamespacedAssetProvider(IAssetProvider<T> provider, string ns)
        : base(provider)
    {
        Namespace = ns;
    }

    protected override IEnumerable<string> GetFilenames(string name)
        => base.GetFilenames($@"{Namespace}/{name}");
}
