using BepInEx.Configuration;
using BepInEx;
using UnityEngine;
using System.IO;

namespace Seasons {
    
    public static class SeasonSettings {
        static public Season season;
        static public bool showLeaves;

        static public bool useBakedLights = true;
        static public void Deserialize() {
            ConfigFile file = new ConfigFile(Path.Combine(Paths.ConfigPath, "Seasons.cfg"), true);

            ConfigEntry<Season> cfgSeason = file.Bind("Season Settings", "Season", Season.None);
            season = cfgSeason.Value;
            ConfigEntry<bool> cfgLeaves = file.Bind("Season Settings", "ShowLeaves", true, "Whether to show leaves. I too understand the torment they cause.");
            showLeaves = cfgLeaves.Value;

            // ConfigEntry<bool> cfgBakedLights = file.Bind("Season Settings", "UseBakedLights", true, "Whether to use baked lights or not. Disabling this will make seasons look nicer, but will take up more performance.");
            // useBakedLights = cfgBakedLights.Value;
        }
    }
}