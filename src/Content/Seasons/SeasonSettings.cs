using BepInEx.Configuration;
using BepInEx;
using UnityEngine;
using System.IO;

namespace Seasons {
    
    public static class SeasonSettings {
        static public Season season;
        static public void Deserialize() {
            ConfigFile file = new ConfigFile(Path.Combine(Paths.ConfigPath, "Seasons.cfg"), true);

            ConfigEntry<Season> cfgSeason = file.Bind("Season Settings", "Season", Season.Summer);
            season = cfgSeason.Value;
        }
    }
}