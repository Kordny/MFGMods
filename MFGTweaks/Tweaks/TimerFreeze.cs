using HarmonyLib;
using UnityEngine.SceneManagement;

namespace MFGTweaks.Tweaks;

internal class TimerFreeze : BaseTweak
{

    public override string Description => "Freezes and hides timers in game menus";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(TimerFreeze));
    }
    
    private static bool IsMatching => SceneManager.GetActiveScene().name == "Matching";

    [HarmonyPostfix, HarmonyPatch(typeof(CountDownTimer), nameof(CountDownTimer.Start))]
    private static void Hook_CountDownTimer_Start(CountDownTimer __instance)
    {
        if (IsMatching)
            return;
        
        __instance.m_disp = false;
    }
    
    [HarmonyPrefix, HarmonyPatch(typeof(CountDownTimer), nameof(CountDownTimer.Update))]
    private static bool Hook_CountDownTimer_Update()
    {
        return IsMatching;
    }
    
    [HarmonyPrefix, HarmonyPatch(typeof(CountDownTimer), nameof(CountDownTimer.SetDisp))]
    private static void Hook_CountDownTimer_SetDisp(ref bool disp)
    {
        if (IsMatching)
            return;
        
        disp = false;
    }

}
