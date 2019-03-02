using System.Collections.Generic;
using UnityEngine;

namespace Chronometer
{
    public class GalaxyGenStep_Stars : GalaxyGenStep
    {
        private GalaxyGenParams galaxyGenParams = null;

        public override float Order
        {
            get { return 1.0f; }
        }
        public override int SeedPart
        {
            get { return 547895; }
        }

        public float MinStarDistance = 100f;
        public int repositionCounter = 0;

        public override void GenerateNew()
        {
            galaxyGenParams = Current.Game.GalaxyGenParams;
            if (galaxyGenParams == null)
            {
                Debug.LogWarning("galaxyGenParams = null");
                galaxyGenParams = new GalaxyGenParams();
            }

            initStars();

        }

        private void initStars()
        {
            QuadTree<Star> stars = new QuadTree<Star>(8, new Rect(-320000f, -320000f, 640000f, 640000f));
            List<Star> starsList = new List<Star>();

            CumulativeDistributionFunction cdf = new CumulativeDistributionFunction();
            cdf.SetupRealistic(1.0d, 0.02d, galaxyGenParams.GalaxyRadius / 3, galaxyGenParams.GalaxyCoreRadius, 0d, galaxyGenParams.GalaxyFarFieldRadius, 1000);

            Star star;
            double rand;
            int k1 = 2;
            int k2 = 0;
            for (int i = 0; i < galaxyGenParams.StarCount; i++)
            {
                star = new Star();
                rand = cdf.ValFromProb(Rand.Value);
                star.A = rand;
                star.B = rand * getExcentricity(rand);
                star.Angle = getAngularOffset(rand);
                star.Theta = 360f * Rand.Value;
                star.Position = calcPos(star, galaxyGenParams.PertubationCount, galaxyGenParams.PertubationDamp);

                // Check for stars within minDistance
                bool recheckPos;

                do
                {
                    recheckPos = false;

                    Rect rect = new Rect(star.Position.x - MinStarDistance,
                                            star.Position.z - MinStarDistance,
                                            MinStarDistance * 2f,
                                            MinStarDistance * 2f);
                    List<Star> starsInRange = stars.RetrieveObjectsInArea(rect);

                    if(starsInRange.Count > 0)
                    {
                        rand = cdf.ValFromProb(Rand.Value);
                        star.A = rand;
                        star.B = rand * getExcentricity(rand);
                        star.Angle = getAngularOffset(rand);
                        star.Theta = 360f * Rand.Value;
                        star.Position = calcPos(star, galaxyGenParams.PertubationCount, galaxyGenParams.PertubationDamp);

                        repositionCounter++;

                        recheckPos = true;
                    }

                } while (recheckPos);

                
                star.Temp = 2500 + (12500 * Rand.Value - 3000);
                star.Mag = 0.5f + 0.5 * Rand.Value;


                stars.Insert(star);
                starsList.Add(star);
            }

            Current.Galaxy.Stars = stars;
            Current.Galaxy.AllStars = starsList;
            Debug.Log("Repositionings: " + repositionCounter);
        }

        private HashSet<Star> listForStarsToReposition()
        {
            HashSet<Star> repositionSet = new HashSet<Star>();


            return repositionSet;
        }

        private Vector3 calcPos(Star star, int pertN, double pertAmp)
        {
            return OrbitalCalculator.Compute(star.Angle, star.A, star.B, star.Theta, new Vector3(0, 0, 0), pertN, pertAmp);
        }

        private double getExcentricity(double r)
        {
            if (r < galaxyGenParams.GalaxyCoreRadius)
                return 1 + (r / galaxyGenParams.GalaxyCoreRadius) * (galaxyGenParams.EccentricityInner - 1);
            else if (r > galaxyGenParams.GalaxyCoreRadius && r <= galaxyGenParams.GalaxyRadius)
                return galaxyGenParams.EccentricityInner + (r - galaxyGenParams.GalaxyCoreRadius) / (galaxyGenParams.GalaxyRadius - galaxyGenParams.GalaxyCoreRadius) * galaxyGenParams.EccentricityOuter - galaxyGenParams.EccentricityInner;
            else if (r > galaxyGenParams.GalaxyRadius && r < galaxyGenParams.GalaxyFarFieldRadius)
                return galaxyGenParams.EccentricityOuter + (r - galaxyGenParams.GalaxyRadius) / (galaxyGenParams.GalaxyFarFieldRadius - galaxyGenParams.GalaxyRadius) * (1 - galaxyGenParams.EccentricityOuter);
            else return 0;
        }

        private double getAngularOffset(double rad)
        {
            return rad * galaxyGenParams.DeltaAngle;
        }

    }
}
