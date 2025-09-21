using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Konami.VirtualSurround;

namespace MFGTweaks.Tweaks;

public class TitleScreenSoundNoSurround : BaseTweak
{
    
    public override string Description => "Makes title, attract, demo screens use all speakers, which adds sound back for PC players";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(TitleScreenSoundNoSurround));
    }


    [HarmonyPostfix, HarmonyPatch(typeof(SoundManager), nameof(SoundManager.SetDemoVolume))]
    private static void Hook_SoundManager_SetDemoVolume()
    {
        SoundManager.SetHeadPhoneVolume(0.5f);
        SoundManager.SetAttractVolume(1f);

    }

    [HarmonyPostfix, HarmonyPatch(typeof(SoundManager), nameof(SoundManager.SetSpeakerVolume))]
    private static void Hook_SoundManager_SetSpeakerVolume(SoundManager __instance, ref float val)
    {
        if (val == 0f) val = 0.5f;
        if (val == 0.0001f) val = 0.5f;
        float num = (float) (20.0 * Math.Log10((double) val));
        SoundManager._virtualSurroundGain.ch1 = num;
        SoundManager._virtualSurroundGain.ch2 = num;
        SoundManager._virtualSurroundGain.ch3 = num;
        SoundManager._virtualSurroundGain.ch4 = num;
        SoundManager._virtualSurroundGain.ch5 = num;
        SoundManager._virtualSurroundGain.ch6 = num;
        SoundManager._virtualSurroundGain.ch7 = num;
        SoundManager._virtualSurroundGain.ch8 = num;

    }

    [HarmonyPostfix, HarmonyPatch(typeof(SoundManager), nameof(SoundManager.SetHeadPhoneVolume))]
    private static void Hook_SoundManager_SetHeadPhoneVolume(SoundManager __instance, ref float val)
    {
        if (val == 0f) val = 0.5f;
        if (val == 0.0001f) val = 0.5f;
        float num = (float) (20.0 * Math.Log10((double) val));
        SoundManager._virtualSurroundGain.ch1 = num;
        SoundManager._virtualSurroundGain.ch2 = num;
        SoundManager._virtualSurroundGain.ch3 = num;
        SoundManager._virtualSurroundGain.ch4 = num;
        SoundManager._virtualSurroundGain.ch5 = num;
        SoundManager._virtualSurroundGain.ch6 = num;
        SoundManager._virtualSurroundGain.ch7 = num;
        SoundManager._virtualSurroundGain.ch8 = num;

    }



}
