using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace MFGTweaks;

public abstract class BaseTweak
{

    internal ManualLogSource Logger { get; set; }

    internal Harmony Harmony { get; set; }

    internal ConfigFile Config { get; set; }

    public virtual bool EnabledByDefault => true;
    
    public virtual string Description => null;

    public virtual void BindConfig() {}

    public abstract void Initialize();

}
