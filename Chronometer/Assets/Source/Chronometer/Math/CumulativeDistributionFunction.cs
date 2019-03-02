using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chronometer
{
    public class CumulativeDistributionFunction
    {
        public double FMin;
        public double FMax;
        public double FWidth;
        public int NSteps;

        public double IO;
        public double K;
        public double A;
        public double RBulge;

        public List<double> M1;
        public List<double> Y1;
        public List<double> X1;
                    
        public List<double> M2;
        public List<double> Y2;
        public List<double> X2;

        public CumulativeDistributionFunction()
        {
            M1 = new List<double>();
            Y1 = new List<double>();
            X1 = new List<double>();
                             
            M2 = new List<double>();
            Y2 = new List<double>();
            X2 = new List<double>();
        }

        public double ProbFromVal(double fVal)
        {
            double h = 2d * ((FMax - FMin) / NSteps);
            int i = (int)((fVal - FMin) / h);
            double remainder = fVal - i * h;

            return (Y1[i] + M1[i] * remainder);
        }

        public double ValFromProb(double fVal)
        {
            double h = 1d / (Y2.Count - 1);
            int i = (int)(fVal / h);

            double remainder = fVal - i * h;
            double result = Y2[i] + M2[i] * remainder;

            return result;
        }

        public void SetupRealistic(double IO, double k, double a, double RBulge, double min, double max, int nSteps)
        {
            FMin = min;
            FMax = max;
            NSteps = nSteps;

            this.IO = IO;
            K = k;
            A = a;
            this.RBulge = RBulge;

            BuildCDF(NSteps);
        }

        private void BuildCDF(int nSteps)
        {
            double h = (FMax - FMin) / nSteps;
            double x = 0d, y = 0d;

            M1.Clear();
            Y1.Clear();
            X1.Clear();
            M2.Clear();
            Y2.Clear();
            X2.Clear();

            Y1.Add(0d);
            X1.Add(0d);

            for (int i = 0; i < nSteps; i += 2)
            {
                x = (double)(i + 2) * h;
                y += h / 3 * (Intensity(FMin + i * h) + 4 * Intensity(FMin + (i + 1) * h) + Intensity(FMin + (i + 2) * h));

                M1.Add((y - Y1.Last()) / (2 * h));
                X1.Add(x);
                Y1.Add(y);
            }
            M1.Add(0d);

            for (int i = 0; i < Y1.Count; ++i)
            {
                Y1[i] /= Y1.Last();
                M1[i] /= Y1.Last();
            }

            X2.Add(0d);
            Y2.Add(0d);

            double p = 0d;
            h = 1d / nSteps;
            for (int i = 1, k = 0; i < nSteps; ++i)
            {
                p = (double)i * h;

                for (; Y1[k + 1] <= p; ++k)
                {

                }

                y = X1[k] + (p - Y1[k]) / M1[k];

                M2.Add((y - Y2.Last()) / h);
                X2.Add(p);
                Y2.Add(y);
            }
            M2.Add(0d);
        }

        private double IntensityBulge(double R, double IO, double k)
        {
            return IO * System.Math.Exp(-k * System.Math.Pow(R, 0.25d));
        }

        private double IntensityDisc(double R, double IO, double a)
        {
            return IO * System.Math.Exp(-R / a);
        }

        private double Intensity(double x)
        {
            if (x < RBulge)
                return IntensityBulge(x, IO, K);

            return IntensityDisc(x - RBulge, IntensityBulge(RBulge, IO, K), A);
        }
    }
}
