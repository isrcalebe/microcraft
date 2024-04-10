using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using microcraft.Game.Extensions.StreamExtensions;

namespace microcraft.Game.IO.Providers;

public class AssemblyAssetProvider : IAssetProvider<byte[]>
{
    private readonly Assembly assembly;
    private readonly string prefix;

    public IEnumerable<string> AvailableAssets
        => assembly.GetManifestResourceNames().Select(name =>
        {
            name = name[(name.StartsWith(prefix, StringComparison.Ordinal) ? prefix.Length + 1 : 0)..];

            var lastDot = name.LastIndexOf('.');
            var chars = name.ToCharArray();

            for (var i = 0; i < lastDot; i++)
            {
                if (chars[i] == '.')
                    chars[i] = '/';
            }

            return new string(chars);
        });

    public AssemblyAssetProvider(string assemblyName)
    {
        var filePath = Path.Combine(GameEnvironment.GAME_ASSEMBLY_LOCATION, assemblyName);

        assembly = File.Exists(filePath)
            ? Assembly.LoadFrom(filePath)
            : Assembly.Load(Path.GetFileNameWithoutExtension(assemblyName));

        prefix = Path.GetFileNameWithoutExtension(assemblyName);
    }

    public AssemblyAssetProvider(Assembly assembly)
    {
        this.assembly = assembly;
        prefix = assembly.GetName().Name!;
    }

    public AssemblyAssetProvider(AssemblyName assemblyName)
        : this(Assembly.Load(assemblyName))
    {
    }

    public byte[]? Fetch(string name)
    {
        using var input = GetStream(name);

        return input?.ReadAllBytesToArray();
    }

    public virtual async Task<byte[]?> FetchAsync(string name, CancellationToken cancellationToken = default)
    {
        using var input = GetStream(name);

        if (input == null)
            return null;

        return await input.ReadAllBytesToArrayAsync(cancellationToken).ConfigureAwait(false);
    }

    public Stream? GetStream(string name)
    {
        var split = name.Split('/');

        for (var i = 0; i < split.Length - 1; i++)
            split[i] = split[i].Replace('-', '_');

        return assembly?.GetManifestResourceStream($@"{prefix}.{string.Join('.', split)}");
    }

    #region IDisposable Support

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    #endregion
}
