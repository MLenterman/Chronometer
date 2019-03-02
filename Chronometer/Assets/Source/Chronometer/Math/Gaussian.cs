using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chronometer
{
    public class Gaussian
    {

        public Gaussian()
        {

        }

        /*  Box–Muller transform
         *  https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
         */
        public static float RandGaussian(float randA, float RandB, float mu = 0f, float sigma = 1f)
        {
            float val = Mathf.Sqrt(-2f * Mathf.Log(randA)) * Mathf.Sin(2 * Mathf.PI * RandB);
            return val * sigma + mu;
        }

        public static float RandGaussianAsym(float randA, float RandB, float mu = 0f,
            float leftSigma = 1f, float rightSigma = 1f)
        {
            float val = Mathf.Sqrt(-2f * Mathf.Log(randA)) * Mathf.Sin(2 * Mathf.PI * RandB);

            if (val < 0f)
                return val * leftSigma + mu;

            return val * rightSigma + mu;
        }
    }
}
