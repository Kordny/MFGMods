using HarmonyLib;

namespace MFGTweaks.Tweaks;

public class EnableLogging : BaseTweak
{

    public override bool EnabledByDefault => false;

    public override string Description =>
        "Re-enables logging to output_log.txt. The resulting logs can be very large in size, therefore it's disabled by default";

    public override void Initialize()
    {
        Harmony.PatchAll(typeof(EnableLogging));
    }
    
    [HarmonyPostfix, HarmonyPatch(typeof(ResidentFlow), nameof(ResidentFlow.Awake))]
    private static void Hook_ResidentFlow_Awake()
    {
        UnityEngine.Debug.unityLogger.logEnabled = true;
    }

}
