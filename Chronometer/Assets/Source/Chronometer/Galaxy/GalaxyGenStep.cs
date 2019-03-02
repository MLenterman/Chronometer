using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronometer
{
    public abstract class GalaxyGenStep
    {
        public GalaxyGenStepDefinition definition;

        public abstract float Order { get; }

        public abstract int SeedPart { get; }

        public abstract void GenerateNew();

        public virtual void GenerateWithoutGalaxy()
        {
            GenerateNew();
        }
    }   
}


