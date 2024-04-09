using System.Diagnostics.CodeAnalysis;

namespace microcraft.Game.Extensions.ObjectExtensions;

public static class ObjectExtensions
{
    [return: NotNull]
    public static T AsNonNull<T>(this T? source)
        => source!;

    public static bool IsNull<T>([NotNullWhen(false)] this T obj)
        => ReferenceEquals(obj, null);

    public static bool IsNotNull<T>([NotNullWhen(true)] this T obj)
        => !ReferenceEquals(obj, null);
}
