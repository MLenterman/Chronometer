using UnityEngine;
using System.Collections.Generic;

/*
 * TODO:
 * Remove() -   Check if index can be cheaply grabbed from dictionary to pass to resetIndices() as startIndex.
 *              Is the overhead of grabbing and checking the index less than the overhead of resetting all indices?
 *              
 * Mods     -   Add grabbing defs from mods here or in modloader?           
 * 
 */

namespace Chronometer
{
    public static class DefinitionDictionary<T> where T : Definition, new()
    {
        private static List<T> defsList = new List<T>();
        private static Dictionary<string, T> defsByName = new Dictionary<string, T>();

        public static IEnumerable<T> AllDefs
        {
            get { return defsList; }
        }

        public static int DefsCount
        {
            get { return defsList.Count; }
        }

        public static void Add(T def)
        {
            if(defsList.Count > Definition.MaxDefCount)
            {
                Debug.LogError("Max index reached " + typeof(T) + ", over " + Definition.MaxDefCount);
                return;
            }

            while (defsByName.ContainsKey(def.DefName))
            {
                Debug.LogError("Adding duplicate " + typeof(T) + " name: " + def.DefName);
                def.DefName += Rand.Value * 100f;
            }

            defsList.Add(def);
            defsByName.Add(def.DefName, def);

            def.Index = (ushort)(defsList.Count - 1);
        }

        public static void Add(IEnumerable<T> defs)
        {
            foreach (T def in defs)
                Add(def);
        }

        public static T GetByName(string defName)
        {
            T def = null;

            if (!defsByName.TryGetValue(defName, out def))
                Debug.LogError("Could not find " + typeof(T) + " with the name: " + defName);

            return def;
        }

        public static T GetByIndex(ushort index)
        {
            if (index >= defsList.Count)
            {
                Debug.LogError("Could not find " + typeof(T) + " with the index: " + index);
                return null;
            }

            return defsList[index];
        }

        public static T GetByHash(int hash)
        {
            foreach(T item in AllDefs)
            {
                T def = item;

                if (def.GetHashCode() == hash)
                    return def;
            }

            Debug.LogError("Could not find " + typeof(T) + " with the hash: " + hash);
            return null;
        }

        public static void Clear()
        {
            defsList.Clear();
            defsByName.Clear();
        }

        public static void ClearCache()
        {
            foreach (T item in AllDefs)
            {
                T def = item;
                def.ClearCache();
            }
        }

        public static void CheckAllDefsForErrors()
        {
            foreach(T item in AllDefs)
            {
                T def = item;

                if (!def.IgnoreErrors)
                {
                    foreach (string error in def.CheckForErrors())
                        Debug.LogError("Error in def: " + error);
                }
            }
        }

        private static void Remove(T def)
        {
            defsByName.Remove(def.DefName);
            defsList.Remove(def);
            resetIndices();
        }

        private static void resetIndices(int startIndex = 0)
        {
            for (int i = startIndex; i < defsList.Count; i++)
                defsList[i].Index = (ushort)i;
        }
    }
}
