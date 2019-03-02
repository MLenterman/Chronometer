using UnityEngine;
using System;

namespace Chronometer
{
    [CreateAssetMenu(fileName = "GalaxyObjectDef", menuName = "Defs/GalaxyObjectDef", order = 1)]
    public class GalaxyObjectDef : Definition
    {
        public string GalaxyObjectClassString;
        public Type GalaxyObjectClass;

        public GameObject Prefab;

        public override void ClearCache()
        {
            throw new NotImplementedException();
        }
    }
}
