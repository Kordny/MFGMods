using HarmonyLib;
using System;
using UnityEngine;

namespace MFGTweaks.Tweaks;

public class MuteBGM : BaseTweak
{

    public override string Description => "Lets you mute in-game BGM using Alt + M";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(MuteBGM));
    }

    public static bool BGMmuted = false;
    public static string lastPlayed;


    [HarmonyPrefix, HarmonyPatch(typeof(SoundManager), nameof(SoundManager.PlayBgm), new Type[] { typeof(bool), typeof(string), typeof(float) })]
    private static bool Hook_SoundManager_PlayBGM(ref bool isPlay, ref string label, ref float delay)
    {
        lastPlayed = label;
        if (isPlay && !BGMmuted)
        {
            SoundManager.PlayBgm(label, delay);
        }
        else
        {
            SoundManager.StopBgm();
        }
        return false;
    }

    [HarmonyPrefix, HarmonyPatch(typeof(SoundManager), nameof(SoundManager.PlayBgm), new Type[] { typeof(string), typeof(float) })]
    private static bool Hook_SoundManager_PlayBGM2(ref string label, ref float delay)
    {
        lastPlayed = label;
        return !BGMmuted;
    }

    [HarmonyPostfix, HarmonyPatch(typeof(ResidentFlow), nameof(ResidentFlow.Update))]
    private static void Hook_ResidentFlow_Update(ResidentFlow __instance)
    {
        PluginUpdate();
    }

    private static void PluginUpdate()
    {
        var isAltPressed = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
        if (isAltPressed)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (!BGMmuted)
                {
                    BGMmuted = !BGMmuted;
                    SoundManager.StopBgm();
                }
                else
                {
                    BGMmuted = !BGMmuted;
                    SoundManager.PlayBgm(lastPlayed, 0f);
                }
            }
        }

    }

}
