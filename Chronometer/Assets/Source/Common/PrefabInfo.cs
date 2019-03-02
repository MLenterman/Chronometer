using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chronometer
{
    public class PrefabInfo : MonoBehaviour
    {
        private static GameObject prefabPoolInstance = null;

        public PrefabPool PrefabPool;

        private static GameObject getPrefabPoolInstance()
        {
            if (prefabPoolInstance == null)
            {
                prefabPoolInstance = new GameObject("Prefab Pool");
                DontDestroyOnLoad(prefabPoolInstance);
            }

            return prefabPoolInstance;
        }
    }
}
