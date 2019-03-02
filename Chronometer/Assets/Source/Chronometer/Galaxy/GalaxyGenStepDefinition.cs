using UnityEngine;
using UnityEditor;
using System;

namespace Chronometer
{
    [CreateAssetMenu(fileName = "GalaxyGenStepDefinition", menuName = "Definitions/GalaxyGenStepDefinition", order = 1)]
    public class GalaxyGenStepDefinition : Definition
    {
        public Type GalaxyGenStepClass = typeof(GalaxyGenStep);

        public GalaxyGenStep GalaxyGenStep;

        public float Order = 1f;


        public ushort SeedPart = 0;

        public override void ClearCache()
        {
            throw new NotImplementedException();
        }
    }
}