using System.Collections.Generic;
using UnityEngine;

namespace Chronometer
{
    public class Star : OrbitalBody, IQuadTreeObject
    {
        public double Temp;
        public double Mag;

        public Star() { }

        public Vector2 GetPosition()
        {
            return new Vector2(Position.x, Position.z);
        }

    }
}
