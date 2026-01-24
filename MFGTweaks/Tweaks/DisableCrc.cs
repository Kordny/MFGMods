using HarmonyLib;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace MFGTweaks.Tweaks;

public class DisableCRC : BaseTweak
{
    public override string Description => "Disables CRC checks for Addressable Asset Bundles";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(DisableCRC));
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(AssetBundleRequestOptions), "Crc", MethodType.Getter)]
    private static bool Prefix_GetCrc(ref uint __result)
    {
        __result = 0u;
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(AssetBundleRequestOptions), "UseCrcForCachedBundle", MethodType.Getter)]
    private static bool Prefix_GetUseCrcForCachedBundle(ref bool __result)
    {
        __result = false;
        return false;
    }
}
