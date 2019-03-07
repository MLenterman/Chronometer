using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chronometer
{
    public class GalaxyGenStep_Planets : GalaxyGenStep
    {
        public override float Order
        {
            get { return 2.0f; }
        }

        public override int SeedPart
        {
            get { return 25478; }
        }

        public float Eccentricity = 0f;

        public int MinPlanetCount = 1;
        public int MaxPlanetCount = 25;

        public float MinDistanceToSun = 100f;
        public float MaxDistanceToSun = 1000f;

        private List<Star> stars;

        public override void GenerateNew()
        {
            stars = Current.Galaxy.AllStars;

            initPlanets();
        }

        private void initPlanets()
        {
            List<Planet> allPlanets = new List<Planet>();

            Star parentStar;
            Planet planet;
            int planetCount;

            for(int i = 0; i < stars.Count; i++)
            {
                parentStar = stars[i];
                planetCount = Rand.RangeInclusive(MinPlanetCount, MaxPlanetCount);

                for(int j = 0; j < planetCount; j++)
                {
                    planet = new Planet();
                    planet.A = Rand.RangeInclusive(MinDistanceToSun, MaxDistanceToSun);
                    planet.B = planet.A * Eccentricity;
                    planet.Angle = 360f * Rand.Value;
                    planet.Theta = 360f * Rand.Value;
                    planet.Position = calcPos(parentStar, planet);

                    planet.ParentOrbital = parentStar;

                    parentStar.childOrbitals.Add(planet);
                    allPlanets.Add(planet);
                }
            }

            Current.Galaxy.AllPlanets = allPlanets;
        }

        private Vector3 calcPos(Star parentStar, Planet planet)
        {
            return OrbitalCalculator.Compute(planet.Angle, planet.A, planet.B, planet.Theta, parentStar.Position, 0, 0f);
        }
    }
}
