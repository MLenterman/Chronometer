using UnityEngine;
using System.Collections;


namespace Chronometer
{
    public class Ore
    {
        public OreDefinition Def;

        public string Name
        {
            get { return Def.Name; }
        }

        public string ShortDescription
        {
            get { return Def.ShortDescription; }
        }

        public string FullDescription
        {
            get { return Def.FullDescription; }
        }

        public Texture2D SmallIcon
        {
            get { return Def.SmallIcon; }
        }

        public Texture2D LargeIcon
        {
            get { return Def.LargeIcon; }
        }

        public bool IsVisible
        {
            get { return Def.IsVisible; }
        }

        public bool ShowOnHotbar
        {
            get { return Def.ShowOnHotbar; }
        }

        public bool ShowMaxCapacity
        {
            get { return Def.ShowMaxCapacity; }
        }

        public float Order
        {
            get { return Def.Order; }
        }

        public float Abundance
        {
            get { return Def.Abundance; }
        }
    }
}
