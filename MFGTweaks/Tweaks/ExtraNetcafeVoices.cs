using HarmonyLib;
using MFG.Types;
using System;

namespace MFGTweaks.Tweaks;

public class ExtraNetcafeVoices : BaseTweak
{

    public override string Description => "Makes the girls randomly say special NetCafe specific voice lines on touch. Only some girls have them";
    public override bool EnabledByDefault => true;

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(ExtraNetcafeVoices));
    }


    [HarmonyPrefix, HarmonyPatch(typeof(SoundManager), nameof(SoundManager.GetExtraVoiceLabel))]
    private static bool Hook_SoundManager_GetExtraVoiceLabel(ref string label, ref CharaType type, ref string __result)
    {
        Random random = new Random();
        int r = random.Next(0, 2);
        if (SoundManager.ExVoiceEnableCharas.Contains(type) && r == 1)
        {
            switch (label)
            {
                case "VOX_GXX_MENU1":
                    {
                        int num = UnityEngine.Random.Range(0, 3);
                        if (num != 0)
                        {
                            __result = label + $"N_{num + 1}";
                            return false;
                        }

                        __result = label + "N";
                        return false;
                    }
                case "VOX_GXX_TOUCH_1":
                case "VOX_GXX_TOUCH_2":
                case "VOX_GXX_TOUCH_3":
                case "VOX_GXX_CONTINUE1":
                case "VOX_GXX_CONTINUE2":
                case "VOX_GXX_END1":
                    __result = label + "N";
                    return false;
            }
        }

        __result = label;
        return false;
    }


}
