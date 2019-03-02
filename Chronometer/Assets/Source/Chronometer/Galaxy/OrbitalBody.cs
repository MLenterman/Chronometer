using UnityEngine;
using System.Collections;

namespace Chronometer
{
    public class OrbitalBody : PriorityQueueNode
    {
        public OrbitalBody ParentOrbital;

        public string Name;

        public double Theta;
        public double Angle;
        public double A;
        public double B;
        public Vector3 Position;
    }
}
