using HarmonyLib;
using UnityEngine.SceneManagement;

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
        bool flag = true;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene sceneAt = SceneManager.GetSceneAt(i);
            if (sceneAt.name == "TestModeMain" || sceneAt.name == "Boot")
            {
                flag = false;
            }
        }
        if (flag) __result = true;

    }

}
