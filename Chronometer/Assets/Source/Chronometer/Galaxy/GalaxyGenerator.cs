using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chronometer;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Chronometer
{
    public static class GalaxyGenerator
    {
        private static List<GalaxyGenStep> galaxyGenStepsOrdered = new List<GalaxyGenStep>();

        public static List<GalaxyGenStep> GalaxyGenStepsDirty = new List<GalaxyGenStep>();

        public static Galaxy GenerateGalaxy(int seed)
        {
            Debug.Log("GalaxyGenerator starting...");

            if (GalaxyGenStepsDirty.Count == 0)
            {
                Debug.LogWarning("GalaxyGenerator has no GalaxyGenSteps!");
                Current.Galaxy = null;
                return null;
            }

            Current.Galaxy = new Galaxy();
            orderGalaxyGenSteps();

            foreach (GalaxyGenStep step in galaxyGenStepsOrdered)
            {
                /* Replace with own RandGenWrapper
             * -> Combine global seed with seedpart from gen step
             * -> push combined seed on the stack before generation step
             * -> pop combined seed off the stack after step is done
             * -> after all steps are done, global seed should be left on the stack
             */
                Debug.Log("GalaxyGenStep: " + step.GetType());

                Rand.PushSeed(step.SeedPart + seed);
                step.GenerateNew();
                Rand.PopSeed();
                Debug.Log("GalaxyGenStep: " + step.GetType() + " FINISHED!");
            }

            // Call Finalize ish functions for Galaxy stuff

            return Current.Galaxy;
        }

        private static void orderGalaxyGenSteps()
        {  
            galaxyGenStepsOrdered.Clear();
            galaxyGenStepsOrdered = GalaxyGenStepsDirty.OrderBy(o => o.Order).ToList();
        }

    }
}
