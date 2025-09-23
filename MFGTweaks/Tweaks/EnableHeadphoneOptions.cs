using HarmonyLib;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace MFGTweaks.Tweaks;

public class EnableHeadphoneOptions : BaseTweak
{
    
    public override string Description => "Force enables the option in the ingame menu to change the headphone volume";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(EnableHeadphoneOptions));
    }


    [HarmonyPostfix, HarmonyPatch(typeof(GameOption), nameof(GameOption.PageRefresh))]
    private static void Hook_GameOption_PageRefresh(GameOption __instance)
    {
        __instance.soundIndex.SetActive(true);
        __instance.soundSliderON.SetActive(true);
        __instance.soundSliderOFF.SetActive(false);
    }


}
