using System.Collections.Generic;
using UnityEngine;

namespace Chronometer
{
    public class Star : OrbitalBody, IQuadTreeObject
    {
        public List<OrbitalBody> AllChildOrbitals = new List<OrbitalBody>();
        public List<OrbitalBody> DirectChildOrbitals = new List<OrbitalBody>();

        public string Name;

        public double Temp;
        public double Mag;

        public Star() { }

        public Star(string name)
        {
            Name = name;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(Position.x, Position.z);
        }

    }
}
