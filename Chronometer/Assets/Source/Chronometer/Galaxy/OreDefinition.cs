using UnityEngine;
using System.Collections;
using System;

/*
 * ORES:
 * -Carbon       -   1.0
 * -Sodium       -   0.004
 * -Silicon      -   0.14
 * -Aluminium    -   0.01
 * Potassium
 * Calcium      -   0.014
 * Strontium
 * -Copper       -   0.000012
 * -Silver      -   0.0000002
 * -Gold        -   0.00000015
 * Barium
 * -Zinc         -   0.00006
 * Mercury
 * -Tin         -   0.000003
 * -Lead         -   0.000002
 * Antimony
 * Cadmium
 * Bismuth
 * -Iron         -   0.22
 * -Cobalt       -   0.0006
 * -Nickel       -   0.012
 * -Magnesium    -   0.12
 * Manganese    -   0.0016
 * -Titanium     -   0.0006
 * Sulfur       -   0.1
 * -Tungsten    -   0.0000006
 * -Uranium     -   0.0000002
 * Zirconium
 * Vanadium     -   0.0002
 */

namespace Chronometer
{
    [CreateAssetMenu(fileName = "OreDefinition", menuName = "Definitions/OreDefinition", order = 1)]
    public class OreDefinition : Definition
    {
        //Some sort of Type attribute
        public Type DefinitionClass = typeof(Ore);

        public Ore OreClass;

        public string Name = "";
        public string ShortDescription = "";
        public string FullDescription = "";

        public Texture2D SmallIcon;
        public Texture2D LargeIcon;

        public bool IsVisible = true;
        public bool ShowOnHotbar = false;
        public bool ShowMaxCapacity = false;
        public float Order = 1f;

        public float Abundance = 1f;

        public override void ClearCache()
        {
            throw new System.NotImplementedException();
        }
    }
}
