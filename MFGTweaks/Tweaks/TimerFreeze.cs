using HarmonyLib;

namespace MFGTweaks.Tweaks;

internal class TimerFreeze : BaseTweak
{

    public override string Description => "Freezes and hides timers in game menus";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(TimerFreeze));
    }

    [HarmonyPostfix, HarmonyPatch(typeof(CountDownTimer), nameof(CountDownTimer.Start))]
    private static void Hook_CountDownTimer_Start(CountDownTimer __instance)
    {
        __instance.m_disp = false;
    }
    
    [HarmonyPrefix, HarmonyPatch(typeof(CountDownTimer), nameof(CountDownTimer.Update))]
    private static bool Hook_CountDownTimer_Update()
    {
        return false;
    }
    
    [HarmonyPrefix, HarmonyPatch(typeof(CountDownTimer), nameof(CountDownTimer.SetDisp))]
    private static void Hook_CountDownTimer_SetDisp(ref bool disp)
    {
        disp = false;
    }
    
}
