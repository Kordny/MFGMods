using HarmonyLib;

namespace MFGTweaks.Tweaks;

public class ModeSelectAfterContinue : BaseTweak
{

    public override string Description => "Allows selecting game mode after continue";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(ModeSelectAfterContinue));
    }
    
    [HarmonyPostfix, HarmonyPatch(typeof(PlayerConfigGameData), nameof(PlayerConfigGameData.ResetForContinueStart))]
    private static void Hook_PlayerConfigGameData_ResetForContinueStart(PlayerConfigGameData __instance)
    {
        __instance.IsContinued = false;
    }

}
