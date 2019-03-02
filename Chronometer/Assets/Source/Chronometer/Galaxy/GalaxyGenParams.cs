using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chronometer
{
    public class GalaxyGenParams
    {
        public int StarCount = 8000;
               
        public double DeltaAngle = 0.0005f;
               
        public double EccentricityInner = 0.95f;
        public double EccentricityOuter = 1.0f;

        public double GalaxyRadius = 13000;
        public double GalaxyCoreRadius = 8000;
        public double GalaxyFarFieldRadius = 16000;

        public int PertubationCount = 2;
        public double PertubationDamp = 40;

        public bool HasDarkMatter = false;

        public int Seed = 100;

        public GalaxyGenParams()
        {

        }
    }
}
