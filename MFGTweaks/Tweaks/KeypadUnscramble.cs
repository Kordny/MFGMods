using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

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
        new System.Random().Next(14);
        List<int> list = new List<int>();
        for (int j = 0; j < 14; j++)
        {
            list.Add(0);
        }
        __result = list.OrderBy((int i) => Guid.NewGuid()).ToList();
    }



}
