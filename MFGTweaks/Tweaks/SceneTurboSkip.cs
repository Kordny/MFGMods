using HarmonyLib;
using MFG.GameRequest;
using System.Collections.Generic;
using System.Threading;
using TestModeSkeleton;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MFGTweaks.Tweaks;

public class SceneTurboSkip : BaseTweak
{
    
    public override string Description => "Skips a lot of scenes to make playing faster";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(SceneTurboSkip));
    }


    [HarmonyPostfix, HarmonyPatch(typeof(GachaPlayFlow), nameof(GachaPlayFlow.GachaPlay_Init))]
    private static void Hook_GachaPlayFlow_GachaPlay_Init(GachaPlayFlow __instance)
    {
        __instance.IsForceSkip = true;
    }

    [HarmonyPrefix, HarmonyPatch(typeof(GachaPlayFlow), nameof(GachaPlayFlow.GachaPlay_Init))]
    private static void Hook_GachaPlayFlow_GachaPlay_Init2(GachaPlayFlow __instance)
    {
        __instance.IsForceSkip = true;
    }

    [HarmonyPrefix, HarmonyPatch(typeof(GachaPlayFlow), nameof(GachaPlayFlow.OnInitialize))]
    private static void Hook_GachaPlayFlow_OnInitialize(GachaPlayFlow __instance)
    {
        __instance.IsForceSkip = true;
    }

    [HarmonyPrefix, HarmonyPatch(typeof(GameoverFlow), nameof(GameoverFlow.OnExecute))]
    private async static void Hook_GameoverFlow_OnExecute(GameoverFlow __instance)
    {
        await __instance.ExitProcess();
        __instance.GotoNextScene("Logo");
    } 
    
    [HarmonyPrefix, HarmonyPatch(typeof(LogoFlow), nameof(LogoFlow.OnExecute))]
    private static void Hook_LogoFlow_OnExecute(LogoFlow __instance)
    {
        SoundManager.SetDemoVolume();
        GameDataManager.Reset();
        GameUtility.OnBeginFirstGameFlow();
        ResidentFlow.GetInstance().SetInGameDisp(isDisp: true);
        ResidentFlow.GetInstance().GetMahjongActorNoticeManager().SetDisp(isLowerDisp: false, isUpperDisp: false);
        LogoFlow.IsLogoSceneOnceDisplayed = true;
        __instance.GotoNextScene("Title");
    }



}
