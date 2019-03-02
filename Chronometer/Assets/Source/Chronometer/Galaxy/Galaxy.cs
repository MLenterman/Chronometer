using System.Collections.Generic;
using UnityEngine;

namespace Chronometer
{
    public class Galaxy
    {
        public QuadTree<Star> Stars;
        public List<Star> AllStars;

        public FactionManager FactionManager;
    }

}
