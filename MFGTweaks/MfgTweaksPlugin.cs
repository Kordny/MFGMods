using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace MFGTweaks;

[BepInPlugin(PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class MfgTweaksPlugin : BaseUnityPlugin
{

    private const string PLUGIN_GUID = "jp.konmai.mfgtweaks";

    private Harmony _harmony;

    private List<BaseTweak> _tweaks = [];

    private void Awake()
    {
        _harmony = new Harmony(PLUGIN_GUID);
        InstantiateTweaks();

        foreach (var tweak in _tweaks)
        {
            var tweakName = tweak.GetType().Name;

            var configEntry = Config.Bind("Tweaks", tweakName, tweak.EnabledByDefault, BuildDescription(tweak));
            tweak.BindConfig();
            if (!configEntry.Value)
                continue;
            
            Logger.LogMessage($"Initializing {tweakName}...");
            try
            {
                tweak.Initialize();
            }
            catch (Exception e)
            {
                
                Logger.LogError($"Failed to initialize {tweakName}: {e}");
            }
        }
    }

    private void InstantiateTweaks()
    {
        var tweakClasses = from type in Assembly.GetExecutingAssembly()
                                                .GetTypes()
                           where type.IsSubclassOf(typeof(BaseTweak))
                           select type;
        foreach (var tweakClass in tweakClasses)
        {
            var constructor = tweakClass.GetConstructor([]);
            if (constructor == null)
            {
                Logger.LogWarning(
                    $"Tweak class {tweakClass.Name} does not have a constructor without arguments, skipping.");
                continue;
            }

            var tweak = (BaseTweak) constructor.Invoke([]);
            tweak.Logger = Logger;
            tweak.Config = Config;
            tweak.Harmony = _harmony;
            _tweaks.Add(tweak);
        }
    }

    private string BuildDescription(BaseTweak tweak)
    {
        var desc = $"Enable \"{tweak.GetType().Name}\"";
        if (tweak.Description != null)
        {
            desc += $"\n{tweak.Description}";
        }

        return desc;
    }

}
