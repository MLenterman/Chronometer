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
        private List<GameObject> planets = new List<GameObject>();
        private List<GameObject> uiList = new List<GameObject>();
        private List<GameObject> orbitalDrawers = new List<GameObject>();

        public GameObject StarPrefab;
        public GameObject OrbitPrefab;
        public int SolarSystemCount;
        public int Seed;
        public int GalaxyRadius;

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
            GalaxyGenerator.GalaxyGenStepsDirty.Add(new GalaxyGenStep_Planets());
            Current.Game.Galaxy = GalaxyGenerator.GenerateGalaxy(Seed);

            List<Star> temp = Current.Game.Galaxy.Stars.RetrieveObjectsInArea(new Rect(-320000f, -320000f, 640000f, 640000f));

            for (int i = 0; i < temp.Count; i++)
            {
                solarSystems.Add(Instantiate(StarPrefab, temp[i].Position, StarPrefab.transform.rotation));
                Color tempColor = Mathf.CorrelatedColorTemperatureToRGB((float)temp[i].Temp);
                float tempMag = (float)temp[i].Mag;
                solarSystems[i].GetComponent<SpriteRenderer>().color = new Color(tempColor.r * tempMag, tempColor.g * tempMag, tempColor.b * tempMag, tempColor.a);

                for(int j = 0; j < temp[i].childOrbitals.Count; j++)
                {
                    Planet planet = (Planet)temp[i].childOrbitals[j];
                    GameObject obj = Instantiate(StarPrefab, planet.Position, StarPrefab.transform.rotation);
                    planets.Add(obj);
                    obj.transform.SetParent(solarSystems[i].transform);
                    obj.GetComponent<SpriteRenderer>().color = Color.blue;
                    Vector3 scale = obj.transform.localScale;
                    obj.transform.localScale = new Vector3(scale.x * 0.1f, scale.y * 0.1f, scale.z * 0.1f);
                }
            }

            for(int i = 0; i < Current.Galaxy.AllPlanets.Count; i++)
            {
                GameObject obj = Instantiate(OrbitPrefab, Current.Galaxy.AllPlanets[i].ParentOrbital.Position, OrbitPrefab.transform.rotation);
                OrbitCircleDrawer orbit = obj.GetComponent<OrbitCircleDrawer>();
                orbitalDrawers.Add(obj);
                orbit.Radius = Vector3.Distance(Current.Galaxy.AllPlanets[i].Position, Current.Galaxy.AllPlanets[i].ParentOrbital.Position);
                orbit.Draw();

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