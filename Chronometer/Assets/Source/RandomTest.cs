using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Chronometer;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;

namespace Chronometer
{
    public class RandomTest : MonoBehaviour
    {
        private List<GameObject> solarSystems = new List<GameObject>();
        private List<GameObject> uiList = new List<GameObject>();

        public GameObject StarPrefab;
        public int SolarSystemCount;
        public int Seed;
        public int GalaxyRadius;

        FixedPriorityQueue<Star> queue = new FixedPriorityQueue<Star>();

        public GameObject Camera;
        //public Texture2D MapIcon;
        //public GameObject iconPrefab;
        //public GameObject MapNamePrefab;

        private Vector3 minScale = Vector3.one * 1f;
        private Vector3 maxScale = Vector3.one * 0.01f;

        void Start ()
        {
            DefinitionLoader.LoadDefinitions();
            Current.Game = new Game();
            //Current.Game.TickManager = new TickManager();

            Rand.PushSeed(Seed);
            InitGalaxyGenParams();
            Rand.PopSeed();

            // GalaxyGenerator
            GalaxyGenerator.GalaxyGenStepsDirty.Add(new GalaxyGenStep_Stars());
            Current.Game.Galaxy = GalaxyGenerator.GenerateGalaxy(Seed);
            List<Star> temp = Current.Game.Galaxy.Stars.RetrieveObjectsInArea(new Rect(-320000f, -320000f, 640000f, 640000f));

            for (int i = 0; i < temp.Count; i++)
            {
                solarSystems.Add(Instantiate(StarPrefab, temp[i].Position, StarPrefab.transform.rotation));
                //uiList.Add(Instantiate(iconPrefab, temp[i].Pos, iconPrefab.transform.rotation));
                //uiList[i].transform.SetParent(GameObject.Find("MapCanvas").transform);
                //uiList[i].GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);

                Color tempColor = Mathf.CorrelatedColorTemperatureToRGB((float)temp[i].Temp);
                float tempMag = (float)temp[i].Mag;
                solarSystems[i].GetComponent<SpriteRenderer>().color = new Color(tempColor.r * tempMag, tempColor.g * tempMag, tempColor.b * tempMag, tempColor.a);
            }
        }

        void Update ()
        {
            //Current.Game.TickManager.ManagerUpdate();

            // Scale stars down when camera is closer to them
            float distance = Camera.transform.position.y;
            float norm = (distance - 50f) / (20000f - 50);
            norm = Mathf.Clamp01(norm);
            Vector3 globalScale = Vector3.Lerp(maxScale, minScale, norm);
        }

        void OnDrawGizmos()
        {
            if (Current.Galaxy != null)
            {
                Color color = Color.blue;
                color.a = 0.1f;
                Gizmos.color = color;
                Current.Galaxy.Stars.DrawDebug();
            }
        }

        private void InitGalaxyGenParams()
        {
            Current.Game.GalaxyGenParams = new GalaxyGenParams();
            Current.Game.GalaxyGenParams.Seed = Seed;
            Current.Game.GalaxyGenParams.DeltaAngle = Rand.RangeInclusive(0.0000f, 0.001f);
            Current.Game.GalaxyGenParams.EccentricityInner = Rand.RangeInclusive(0.1f, 1.0f);
            Current.Game.GalaxyGenParams.EccentricityOuter = Rand.RangeInclusive(0.1f, 1.0f);
            Current.Game.GalaxyGenParams.GalaxyCoreRadius = Rand.RangeInclusive(20000f, 120000f);
            Current.Game.GalaxyGenParams.GalaxyRadius = GalaxyRadius;
            Current.Game.GalaxyGenParams.GalaxyFarFieldRadius = GalaxyRadius * 2;
            Current.Game.GalaxyGenParams.PertubationCount = Rand.RangeInclusive(1, 8);
            Current.Game.GalaxyGenParams.PertubationDamp = Rand.RangeInclusive(5f, 50f); ;
            Current.Game.GalaxyGenParams.StarCount = SolarSystemCount;
        }
    }  
}