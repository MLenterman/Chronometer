using UnityEngine;
using System.Collections;

namespace Chronometer
{
    public abstract class Civic
    {
        public CivicDefinition CivicDefinition;

        public Texture2D Icon
        {
            get { return CivicDefinition.Icon; }
        }

        public string Name
        {
            get { return CivicDefinition.Name; }
        }

        public string ShortDescription
        {
            get { return CivicDefinition.ShortDescription; }
        }

        public string FullDescription
        {
            get { return CivicDefinition.FullDescription; }
        }

    }
}
