using UnityEngine;

namespace Chronometer
{
    public static class OrbitalCalculator
    {
        public static Vector3 Compute(double angle, double a, double b, double theta, Vector3 p, int pertN, double pertAmp)
        {
            double   beta = -angle,
                    alpha = theta * (System.Math.PI / 180);

            double   cosalpha = System.Math.Cos(alpha),
                    sinalpha = System.Math.Sin(alpha),
                    cosbeta = System.Math.Cos(beta),
                    sinbeta = System.Math.Sin(beta);

            Vector3 pos = new Vector3((float)(p.x + (a * cosalpha * cosbeta - b * sinalpha * sinbeta)), 0f,
                                        (float)(p.z + (a * cosalpha * sinbeta + b * sinalpha * cosbeta)));

            // Add small perturbations to create more spiral arms
            
            if (pertAmp > 0 && pertN > 0)
            {
                pos.x += (float)((a / pertAmp) * System.Math.Sin(alpha * 2d * pertN));
                pos.z += (float)((a / pertAmp) * System.Math.Cos(alpha * 2d * pertN));
            }
            
            return pos;
        }
    }
}
