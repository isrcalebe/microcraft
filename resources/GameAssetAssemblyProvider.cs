using System.Reflection;

namespace microcraft.Game.Resources;

public static class GameAssetAssemblyProvider
{
    public static Assembly Assembly => typeof(GameAssetAssemblyProvider).Assembly;
}
