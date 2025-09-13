using HarmonyLib;

namespace MFGTweaks.Tweaks;

public class TestModeFromAnywhere : BaseTweak
{

    public override string Description => "Allows entering test mode from anywhere";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(TestModeFromAnywhere));
    }
    
    [HarmonyPostfix, HarmonyPatch(typeof(ResidentFlow), nameof(ResidentFlow.IsTestModeEffective), MethodType.Getter)]
    private static void Hook_ResidentFlow_get_IsTestModeEffective(ref bool __result)
    {
        __result = true;
    }

}
