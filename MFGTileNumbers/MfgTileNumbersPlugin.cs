using System.IO;
using BepInEx;
using BepInEx.Configuration;
using Cysharp.Threading.Tasks;
using HarmonyLib;

namespace MFGTweaks;

[BepInPlugin(PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class MfgTweaksPlugin : BaseUnityPlugin
{

    private const string PLUGIN_GUID = "jp.konmai.mfgtilenumbers";

    private static TexturePatcher _texturePatcher;

    private static string _texPath;

    private static ConfigEntry<bool> _enabled;

    private void Awake()
    {
        _texturePatcher = new TexturePatcher();
        _enabled = Config.Bind("General", "Enabled", true);
        _texPath = Path.Combine(Info.Location, "..", "tilenumbers.png");
        Harmony.CreateAndPatchAll(typeof(MfgTweaksPlugin));
    }

    [HarmonyPostfix, HarmonyPatch(typeof(TileManager), nameof(TileManager.Init))]
    private static void Hook_TileManager_Init_AsyncBuilder(TileManager __instance, ref UniTask __result)
    {
        __result = Hook_TileManager_Init(__instance, __result);
    }

    private static async UniTask Hook_TileManager_Init(TileManager __instance, UniTask originalTask)
    {
        await originalTask;

        if (!_enabled.Value)
            return;

        var patchedTexture = _texturePatcher.PatchTexture(__instance.m_texture.Asset, _texPath);
        __instance.m_haiMaterial.SetTexture(patchedTexture);
    }

}
