using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Chronometer
{
    public sealed class TickManager
    {
        public const int TickIntervalNormal = 1;
        public const int TickIntervalShort = 60;
        public const int TickIntervalMedium = 1440;
        public const int TickIntervalLong = 10080;
        public const int TickIntervalRare = 3679200;

        private bool paused;
        private Stopwatch timer = new Stopwatch();

        public float RealTimeToTick;

        public float CurrentTickTime
        {
            get
            {
                if (paused)
                    return 0f;

                return 1f / (60f * tickRateMultiplier);
            }
        }

        public float tickRateMultiplier = 1f;
        private ulong startOfSessionTicks;
        public ulong gameTicks;

        private TickRegistry tickRegistryNormal;
        private TickRegistry tickRegistryShort;
        private TickRegistry tickRegistryMedium;
        private TickRegistry tickRegistryLong;
        private TickRegistry tickRegistryRare;

        public TickManager()
        {
            tickRegistryNormal = new TickRegistry(TickType.Normal);
            tickRegistryShort = new TickRegistry(TickType.Short);
            tickRegistryMedium = new TickRegistry(TickType.Medium);
            tickRegistryLong = new TickRegistry(TickType.Long);
            tickRegistryRare = new TickRegistry(TickType.Rare);
        }

        public void ManagerUpdate()
        {
            if (!paused)
            {
                float curTickTime = CurrentTickTime;

                if (Mathf.Abs(Time.deltaTime - curTickTime) < curTickTime * 0.1f)
                {
                    RealTimeToTick += curTickTime;
                }
                else
                {
                    RealTimeToTick += Time.deltaTime;
                }

                int num = 0;
                timer.Reset();
                timer.Start();
                while (RealTimeToTick > 0f && (float) num < tickRateMultiplier * 2f)
                {
                    DoTick();
                    RealTimeToTick -= curTickTime;
                    num++;
                    if (timer.ElapsedMilliseconds > 1000f / 22f)
                    {
                        Debug.Log("Cant keep up!");
                        break;
                    }

                }

                if (RealTimeToTick > 0f)
                    RealTimeToTick = 0f;
            }
        }

        public void DoTick()
        {
            int num = 0;
            for (int i = 0; i < 100000; i++)
            {
                num += i;
            }

            gameTicks++;
        }

    }
}
