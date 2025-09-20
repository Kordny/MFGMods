using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace MFGTweaks.Tweaks;

public class KeypadUnscramble : BaseTweak
{
    
    public override string Description => "Unscrambles the keypad pin code keys when logging in";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(KeypadUnscramble));
    }


    [HarmonyPostfix, HarmonyPatch(typeof(TenkeyManager), nameof(TenkeyManager.GetTenkeyPattern))]
    private static void Hook_TenkeyManager_GetTenkeyPattern(ref List<int> __result)
    {
        new System.Random().Next(1);
        List<int> list = new List<int>();
        for (int j = 0; j < 1; j++)
        {
            list.Add(j);
        }
        __result = list;
    }

}
