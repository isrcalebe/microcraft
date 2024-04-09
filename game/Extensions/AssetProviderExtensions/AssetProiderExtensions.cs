using System;
using System.Collections.Generic;
using System.Linq;

namespace microcraft.Game.Extensions.AssetProviderExtensions;

public static class AssetProiderExtensions
{
    private static readonly string[] system_filename_ignore_list =
    {
        "__MACOSX",
        ".DS_Store",
        "Thumbs.db"
    };

    public static IEnumerable<string> ExcludeSystemFilenames(this IEnumerable<string> source)
        => source
            .Where(entry =>
                !system_filename_ignore_list
                .Any(ignoredFile =>
                    entry.Contains(ignoredFile, StringComparison.OrdinalIgnoreCase)));
}
