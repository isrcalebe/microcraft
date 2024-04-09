using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace microcraft.Game.IO.Providers;

public interface IAssetProvider<T> : IDisposable
    where T : class
{
    IEnumerable<string> AvailableAssets { get; }

    T? Fetch(string name);

    Task<T?> FetchAsync(string name, CancellationToken cancellationToken = default);

    Stream? GetStream(string name);
}
