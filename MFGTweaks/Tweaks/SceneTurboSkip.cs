using HarmonyLib;
using BepInEx.Configuration;

namespace MFGTweaks.Tweaks;

public class SceneTurboSkip : BaseTweak
{

    public override bool EnabledByDefault => false;

    public override string Description => "Skips some scenes to make playing faster. Specific scenes can be chosen under [zTurbo Scene Skips]";

    public static ConfigEntry<bool> GachaSkip;
    public static ConfigEntry<bool> GameOverSkip;
    public static ConfigEntry<bool> LogoSkip;
    public static ConfigEntry<bool> CautionSkip;

    public override void Initialize()
    {
        GachaSkip = Config.Bind("zTurbo Scene skips", "Gacha", true, "Skips the gacha screen animation");
        LogoSkip = Config.Bind("zTurbo Scene skips", "Logo", true, "Skips the logos shown before the title screen");
        GameOverSkip = Config.Bind("zTurbo Scene skips", "Gameover", true, "Skips the game over screen");
        CautionSkip = Config.Bind("zTurbo Scene skips", "Caution", true, "Skips the caution screen before home menu");
        Harmony.PatchAll(typeof(SceneTurboSkip));
    }


    [HarmonyPrefix, HarmonyPatch(typeof(GachaPlayFlow), nameof(GachaPlayFlow.GachaPlay_Init))]
    public static void HookPre_GachaPlayFlow_GachaPlay_Init2(GachaPlayFlow __instance)
    {
        if(GachaSkip.Value) __instance.IsForceSkip = true;
    }

    [HarmonyPostfix, HarmonyPatch(typeof(GachaPlayFlow), nameof(GachaPlayFlow.GachaPlay_Init))]
    private static void HookPost_GachaPlayFlow_GachaPlay_Init(GachaPlayFlow __instance)
    {
        if (GachaSkip.Value) __instance.IsForceSkip = true;
    }

    [HarmonyPrefix, HarmonyPatch(typeof(GachaPlayFlow), nameof(GachaPlayFlow.OnInitialize))]
    private static void Hook_GachaPlayFlow_OnInitialize(GachaPlayFlow __instance)
    {
        if (GachaSkip.Value) __instance.IsForceSkip = true;
    }

    [HarmonyPrefix, HarmonyPatch(typeof(GameoverFlow), nameof(GameoverFlow.OnExecute))]
    private static async void Hook_GameoverFlow_OnExecute(GameoverFlow __instance)
    {
        if (GameOverSkip.Value)
        {
            await __instance.ExitProcess();
            __instance.GotoNextScene("Logo");
        }
    }

    [HarmonyPrefix, HarmonyPatch(typeof(LogoFlow), nameof(LogoFlow.OnExecute))]
    private static void Hook_LogoFlow_OnExecute(LogoFlow __instance)
    {
        if (LogoSkip.Value)
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

    [HarmonyPrefix, HarmonyPatch(typeof(CautionSet), nameof(CautionSet.Play))]
    private static bool Hook_CautionSet_Play()
    {
        if (CautionSkip.Value) return false;
        else return true;
    }

}
