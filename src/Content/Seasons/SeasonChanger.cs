using System;
using System.Collections.Generic;
using UnityEngine; 
namespace Seasons {
   
    public static class SeasonChanger {
        // Used to know if it's ok to make certain objects collidable
         public static Season realSeason = Season.Summer;
         static Dictionary<string, Material> matCache = new Dictionary<string, Material>();
        public static LeavesKind SeasonLeavesKind(Season season) 
        {
            switch (season) {
                case Season.Summer:
                    return LeavesKind.Green;
                case Season.Christmas:
                case Season.Winter: 
                    return LeavesKind.None;
                case Season.Fall: 
                    return LeavesKind.Orange;
                default: 
                    return LeavesKind.Green;
            }
        } 
        public static bool SeasonHasLights(Season season) 
        {
            if (season == Season.Christmas)
                return true;
            return false;
        }
        public static bool SnowyTrees(Season season) 
        {
            switch (season) {
                case Season.Christmas: 
                case Season.Winter: 
                    return true;
                default: 
                    return false;
            }
        }
        public static bool FallTreeParticles(Season season) 
        {
            if (season == Season.Fall)
                return true;
            return false;
        }
        public static string FloorMaterialPath(Season season) 
        {
            switch (season) {
                case Season.Fall: 
                    return "objects/forest/materials/pitgroundfall";
                case Season.Christmas: 
                case Season.Winter: 
                    return "objects/forest/materials/pitgroundwinter";
                default: 
                    return "objects/forest/materials/pitground";
            }
        }
        public static void ChangeSmallTree(GameObject tree, Season season) 
        {
            
            for (int i = 0; i < tree.transform.childCount; i++) {
                GameObject child = tree.transform.GetChild(i).gameObject;
                Debug.Log(child.name);
                if (child.name.Contains("smallleaves") && !child.name.Contains("disabled")) {
                    ChangeLeaves(child, season);
                }
                if (child.name == "SmallTree") {
                    ChangeBarkyObject(child, season);
                }
                if (child.name.Contains("Leaf Particles")) {
                    child.SetActive(FallTreeParticles(season));
                }
                if (child.name.Contains("flatpanel")) {
                    ChangePanel(child, season);
                }
            } 
            
        }
        public static void ChangeTreehouse(GameObject treehouse, Season season) 
        {
            for (int i =0; i < treehouse.transform.childCount; i++) {
                GameObject child = treehouse.transform.GetChild(i).gameObject;
                Debug.Log(child.name);
                if (child.name == "Treehouse") {
                    ChangeWintryObject(child, season, "objects/forest/materials/redpanel", 1);
                    
                }
                if (child.name == "SmallTree") {
                    ChangeBarkyObject(child, season);
                }
                if (child.name == "smallleaves") {
                    ChangeLeaves(child, season, true);
                }
                if (child.name.Contains("Leaf Particles")) {
                    child.SetActive(FallTreeParticles(season));
                }
            }
        }
        public static void ChangePanel(GameObject panel, Season season) 
        {
            for (int i =0; i < panel.transform.childCount; i++) 
            {
                GameObject child = panel.transform.GetChild(i).gameObject;
                if (child.name == "FlatPlatform") 
                    ChangeBrownPanelObject(child, season);
            }
        }
        static Material LoadMaterial(string path) {
            if (!matCache.ContainsKey(path))
                matCache.Add(path, Resources.Load<Material>(path));
            return matCache[path];
            
        }
        public static void ChangeWintryObject(GameObject wintryObject, Season season, string matPath, int winterIndex = 1) 
        {
            if (SnowyTrees(season)) 
            {
                wintryObject.GetComponent<Renderer>().materials[winterIndex] = LoadMaterial("objects/forest/materials/pitgroundwinter");
            } else 
            {
                wintryObject.GetComponent<Renderer>().materials[winterIndex] = LoadMaterial(matPath);
            }
        }
        public static void ChangeBrownPanelObject(GameObject panel, Season season, int winterIndex = 0) 
        {
            ChangeWintryObject(panel, season, "objects/forest/materials/darkbrownpanel", winterIndex);
        }
        public static void ChangeBarkyObject(GameObject barkyObject, Season season, int winterIndex = 1) 
        {
            ChangeWintryObject(barkyObject, season, "objects/treeroom/materials/bark", winterIndex);
        }
        public static void ChangeRamp(GameObject ramp, Season season) 
        {
            GameObject realRamp = ramp.transform.Find("Ramp").gameObject;
            if (realRamp != null) {
                ChangeBrownPanelObject(realRamp, season);
            }
        }
        public static void SetSeason(Season season) 
        {
            GameObject smallTrees = GameObject.Find("Level/Forest/SmallTrees");
            if (smallTrees != null) 
            {
                for (int i = 0; i < smallTrees.transform.childCount; i++) 
                {
                    GameObject child = smallTrees.transform.GetChild(i).gameObject;
                    ChangeSmallTree(child, season);
                }
            }
            GameObject treehouseParent = GameObject.Find("Level/Forest/SmallTreeWithTreehouse");
            if (treehouseParent != null) 
            {
                ChangeTreehouse(treehouseParent, season);
            }
            GameObject forest = GameObject.Find("Level/Forest");
            Debug.Log(forest);
            if (forest != null) 
            {
                forest.GetComponent<Renderer>().materials[0] = LoadMaterial(FloorMaterialPath(season));
                for (int i = 0; i < forest.transform.childCount; i++) 
                {
                    GameObject child = forest.transform.GetChild(i).gameObject;
                    Debug.Log(child.name);
                    if (child.name.Contains("flatpanel")) 
                    {
                        ChangePanel(child, season);
                    }
                    if (child.name.Contains("ramp")) 
                    {
                        ChangeRamp(child, season);
                    }
                    if (child.name == "bridge") 
                    {
                        ChangeBrownPanelObject(child, season, 0);
                    }
                }
            }
            GameObject tree = GameObject.Find("Level/treeroom/tree/Tree");
            if (tree != null) 
            {
                ChangeBarkyObject(tree, season);
            }
            GameObject treeRamp = GameObject.Find("Level/Forest/longbranch/ramp");
            if (treeRamp != null) 
            {
                ChangeBarkyObject(treeRamp, season);
            }
            GameObject campsiteRoof = GameObject.Find("Level/Forest/campgroundstructure/roof");
            if (campsiteRoof != null) 
            {
                ChangeWintryObject(campsiteRoof, season, "objects/forest/materials/structureroof", 1);
            }
            ChangeSnowman(season);
        }
        public static void SetChristmasLayerRecursive(GameObject obj) 
        {
            for (int i = 0; i < obj.transform.childCount; i++) {
                GameObject child = obj.transform.GetChild(i).gameObject;
            
                if ((Plugin.inRoom || realSeason == Season.Christmas) && !child.name.Contains("christmastreewhitelights")) {
                    child.layer = 9;
                } else {
                    child.layer = 1;
                }
                SetChristmasLayerRecursive(child);
            }
        }
        public static void ChangeSnowman(Season season) 
        {
            GameObject snowman = GameObject.Find("Level/Forest/snowman");   
            switch (season) {
                case Season.Winter: 
                    // Modded rooms don't care about collision
                    if (!Plugin.inRoom && realSeason != Season.Winter) 
                    {
                        snowman.SetActive(false);
                    } else 
                    {
                        snowman.SetActive(true);
                    }
                    break;
                default: 
                    if (!Plugin.inRoom && realSeason == Season.Winter) 
                    {
                        snowman.SetActive(true); 
                    } else 
                    {
                        snowman.SetActive(false);
                    }
                    break;
            }
        }
        public static void ChangeChristmasActive(Season season) 
        {
            GameObject snow = GameObject.Find("Level/Forest/snow");
            snow.SetActive(season == Season.Christmas || (realSeason == Season.Christmas && !Plugin.inRoom));
            if (snow != null)
                SetChristmasLayerRecursive(snow);
        }
        public static void ChangeLeaves(GameObject leaves, Season season, bool useRedLeaves = false) 
        {
            try {
                if (leaves == null)
                return;
            switch (SeasonLeavesKind(season)) 
            {
                case LeavesKind.Green: 
                    leaves.SetActive(true);
                    Renderer render = leaves.GetComponent<Renderer>();
                    if (render == null) return;
                    render.materials[0] = LoadMaterial("objects/forest/materials/leaves");
                    break;
                case LeavesKind.Orange: 
                    leaves.SetActive(true);
                    Renderer render2 = leaves.GetComponent<Renderer>();
                    if (render2 == null) return;
                    if (useRedLeaves) 
                    {
                        render2.materials[0] = LoadMaterial("objects/forest/materials/fallleavesred");
                    } else 
                    {
                        render2.materials[0] = LoadMaterial("objects/forest/materials/fallleaves");
                    }
                    
                    break;
                case LeavesKind.None: 
                    leaves.SetActive(false);
                    break;
            }
            // i'm tired
            } catch (Exception e) 
            {
                Debug.Log($"ow {e.ToString()}");
            }
            
        }
    } 

    public enum LeavesKind {
        Green,
        Orange,
        None
    }

    public enum Season {
        Summer, 
        Winter,
        Fall,
        Christmas
    }

    public enum SkyState {
        Day, 
        Night,
        Sunrise,
        Overcast
    }
}