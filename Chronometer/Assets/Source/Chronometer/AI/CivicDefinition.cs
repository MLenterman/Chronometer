using UnityEngine;
using System.Collections;
using System;

namespace Chronometer
{
    public class CivicDefinition : Definition
    {
        public Type CivicClass = typeof(Civic);

        public string Name = "";
        public string ShortDescription = "";
        public string FullDescription = "";

        //AttributeList

        public Texture2D Icon;

        public override void ClearCache()
        {
            throw new System.NotImplementedException();
        }
    }
}
