using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Chronometer
{
    public static class DefinitionLoader
    {
        private static List<Definition> AllDefinitions = new List<Definition>();
        private static List<OreDefinition> AllResourceDefinitions = new List<OreDefinition>();

        public static void LoadDefinitions()
        {
            string[] guids = AssetDatabase.FindAssets("t:OreDefinition");

            foreach(string guid in guids)
            {
                OreDefinition def = AssetDatabase.LoadAssetAtPath<OreDefinition>(AssetDatabase.GUIDToAssetPath(guid));
                def.CheckForErrors();
                def.OreClass = new Ore { Def = def };
                AllResourceDefinitions.Add(def);
                DefinitionDictionary<OreDefinition>.Add(def);
            }
        }

     
    }
}
