using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;

/*
 * TODO:
 * Regex    -   Find a way to check if DefName is a valid name in the Unity editor.
 * 
 * Mods     -   Add datastructure for modspecific data.
 * 
 */

namespace Chronometer
{
    public abstract class Definition : ScriptableObject
    {
        public const ushort MaxDefCount = ushort.MaxValue;

        [Tooltip("Definition name used by code")]
        public string DefName = "";

        [Tooltip("Definition label for ingame inspection")]
        public string Label = "";

        [Tooltip("Definition description for ingame inspection")]
        public string Description = "";

        public bool IgnoreErrors = false;

        [HideInInspector]
        public ushort Index = MaxDefCount;
        
        private static readonly Regex defNameRegex = new Regex("^[a-zA-Z0-9\\-_]*$");

        public abstract void ClearCache();

        public void Awake()
        {
            Debug.Log("Definition of " + GetType() + " created!");
        }

        public virtual IEnumerable<string> CheckForErrors()
        {
            if (string.IsNullOrEmpty(DefName))
                yield return GetType() + " DefName is empty or null. Label: " + Label;

            if(!defNameRegex.IsMatch(DefName))
                yield return GetType() + " DefName has invalid characters. Label: " + Label;

            if (string.IsNullOrEmpty(Label))
                yield return GetType() + ", DefName: " + DefName + " has an empty or null Label";

            if (string.IsNullOrEmpty(Description))
                yield return GetType() + ", DefName: " + DefName + " has an empty or null Description";

            yield break;
        }

        public override string ToString()
        {
            return DefName;
        }

        public override int GetHashCode()
        {
            return DefName.GetHashCode();
        }

    }
}
