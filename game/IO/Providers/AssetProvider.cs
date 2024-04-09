using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using microcraft.Game.Extensions.AssetProviderExtensions;

namespace microcraft.Game.IO.Providers;

public class AssetProvider<T> : IAssetProvider<T>
    where T : class
{
    private readonly Dictionary<string, Action?> actionList = new();
    private readonly List<IAssetProvider<T>> providers = new();
    private readonly List<string> searchExtensions = new();

    public IEnumerable<string> AvailableAssets
    {
        get
        {
            lock (providers)
                return providers.SelectMany(provider => provider.AvailableAssets.ExcludeSystemFilenames());
        }
    }

    public AssetProvider(IAssetProvider<T> provider = null)
    {
        if (provider != null)
            AddNestedProvider(provider);
    }

    public virtual void AddNestedProvider(IAssetProvider<T> provider)
    {
        lock (providers)
            providers.Add(provider);
    }

    public virtual void RemoveNestedProvider(IAssetProvider<T> provider)
    {
        lock (providers)
            providers.Remove(provider);
    }

    public virtual async Task<T?> FetchAsync(string name, CancellationToken cancellationToken = default)
    {
        var filenames = GetFilenames(name);

        foreach (var provider in getProviders())
        {
            foreach (var file in filenames)
            {
                var result = await provider.FetchAsync(file, cancellationToken).ConfigureAwait(false);

                if (result != null)
                    return result;
            }
        }

        return default;
    }

    public virtual T? Fetch(string name)
    {
        var filenames = GetFilenames(name);

        return (from provider in getProviders()
                from file in filenames
                select provider.Fetch(file)).OfType<T>().FirstOrDefault();
    }

    public Stream? GetStream(string name)
    {
        var filenames = GetFilenames(name);

        return (
            from provider in getProviders()
            from file in filenames
            select provider.GetStream(file)).FirstOrDefault();
    }

    protected virtual IEnumerable<string> GetFilenames(string name)
    {
        yield return name;

        foreach (var extension in searchExtensions)
            yield return $@"{name}.{extension}";
    }

    public void BindReload(string name, Action? onReload)
    {
        if (onReload == null)
            return;

        if (!actionList.TryAdd(name, onReload))
            throw new InvalidOperationException($"A reload delegate is already bound to the resource \"{name}\".");
    }

    public void AddExtension(string extension)
    {
        extension = extension.Trim('.');

        if (!searchExtensions.Contains(extension))
            searchExtensions.Add(extension);
    }

    private IAssetProvider<T>[] getProviders()
    {
        lock (providers)
            return providers.ToArray();
    }

    #region IDisposable Support

    private bool isDisposed;

    protected virtual void Dispose(bool disposing)
    {
        if (isDisposed) return;

        isDisposed = true;
        lock (providers)
            providers.ForEach(provider => provider.Dispose());
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
