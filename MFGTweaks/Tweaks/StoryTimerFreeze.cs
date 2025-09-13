using System;
using HarmonyLib;
using MFG.StoryPlay;

namespace MFGTweaks.Tweaks;

public class StoryTimerFreeze : BaseTweak
{

    public override string Description => "Disables timer between text messages in story";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(StoryTimerFreeze));
    }
    
    [HarmonyPrefix, HarmonyPatch(typeof(TextDisp), "Exec")]
    private static void Hook_TextDisp_Exec(TextDisp __instance)
    {
        if (__instance.textLength != 0)
        {
            __instance.sec = 1000000;
        }
    }

}
