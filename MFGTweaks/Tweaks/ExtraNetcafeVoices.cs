using HarmonyLib;
using MFG.Types;

namespace MFGTweaks.Tweaks;

public class ExtraNetcafeVoices : BaseTweak
{

    public override string Description => "Makes the girls say special NetCafe specific voice lines on touch. Only Iyo and girls released after her have them";
    public override bool EnabledByDefault => false;

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(ExtraNetcafeVoices));
    }


    [HarmonyPrefix, HarmonyPatch(typeof(SoundManager), nameof(SoundManager.GetExtraVoiceLabel))]
    private static bool Hook_SoundManager_GetExtraVoiceLabel(ref string label, ref CharaType type, ref string __result)
    {
        if (type >= CharaType.Chara13)
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
