using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chronometer
{
    public class OrbitalBody
    {
        public OrbitalBody ParentOrbital;
        public List<OrbitalBody> childOrbitals;

        public string Name;

        public double Theta;
        public double Angle;
        public double A;
        public double B;
        public Vector3 Position;

        public OrbitalBody()
        {
            childOrbitals = new List<OrbitalBody>();
        }
    }
}
