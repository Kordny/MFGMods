using HarmonyLib;
using UnityEngine;

namespace MFGTweaks.Tweaks;

public class UnlimitedHomeButtons : BaseTweak
{
    
    public override string Description => "Allows unlimited use of buttons (such as My Room, Gacha) on home screen";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(UnlimitedHomeButtons));
    }

    [HarmonyPrefix, HarmonyPatch(typeof(DebugFlag), nameof(DebugFlag.IsActive))]
    private static bool Hook_DebugFlag_IsActive(DebugFlag.Flag key, ref bool __result)
    {
        if (key != DebugFlag.Flag.EnableUnlimitedButton)
            return true;

        __result = true;
        return false;
    }
    
    [HarmonyPrefix, HarmonyPatch(typeof(HomeFlow), nameof(HomeFlow.OnInitializeBeforeIntroduction))]
    private static void Hook_HomeFlow_OnInitializeBeforeIntroduction()
    {
        var isContinued = GameDataManager.PlayerConfig.IsContinued;
        GameDataManager.PlayerConfig.ResetForContinueStart();
        GameDataManager.PlayerConfig.IsContinued = isContinued;
    }

}
