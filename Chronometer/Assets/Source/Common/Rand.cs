using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chronometer
{
    public static class Rand
    {
        private static Dictionary<int, Random.State> seedStates;
        private static Stack<int> seedStack;

        static Rand()
        {
            seedStack = new Stack<int>();
            seedStack.Push(DateTime.Now.GetHashCode());
            Random.InitState(seedStack.Peek());

            seedStates = new Dictionary<int, Random.State>();
        }

        public static void PushSeed(int seed)
        {
            addOrUpdateState(seedStack.Peek());
            seedStack.Push(seed);
            useOrInitState(seed);
        }

        public static void PopSeed()
        {
            addOrUpdateState(seedStack.Peek());
            seedStack.Pop();
            useOrInitState(seedStack.Peek());
        }

        public static Vector2 InsideUnitCircle
        {
            get { return Random.insideUnitCircle; }
        }

        public static Vector3 InsideUnitSphere
        {
            get { return Random.insideUnitSphere; }
        }

        public static Vector3 OnUnitSphere
        {
            get { return Random.onUnitSphere; }
        }

        public static Quaternion Rotation
        {
            get { return Random.rotation; }
        }

        public static Quaternion RotationUniform
        {
            get { return Random.rotationUniform; }
        }

        public static float Value
        {
            get { return Random.value; }
        }

        public static float RangeInclusive(float min, float max)
        {
            return Random.Range(min, max);
        }

        public static int RangeInclusive(int min, int max)
        {
            if (max <= min)
                return min;

            return min + Mathf.Abs(Int % (max - min));
        }

        public static bool Bool
        {
            get { return Random.value < 0.5f; }
        }

        public static int Int
        {
            get { return Mathf.Abs((int)(Random.value * int.MaxValue)); }
        }

        public static bool Chance(float chance)
        {
            if (chance <= 0f)
                return false;
            if (chance >= 1f)
                return true;
            return Value < chance;
        }

        public static Color ColorHSV()
        {
            return Random.ColorHSV();
        }

        public static Color ColorHSV(float hueMin, float hueMax)
        {
            return Random.ColorHSV(hueMin, hueMax);
        }

        public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
        {
            return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax);
        }

        public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, 
                                    float valueMin, float valueMax)
        {
            return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, 
                                    valueMin, valueMax);
        }

        public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax,
                                    float valueMin, float valueMax, float alphaMin, float alphaMax)
        {
            return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, 
                                    valueMin, valueMax, alphaMin, alphaMax);
        }

        private static void addOrUpdateState(int seed)
        {
            if (seedStates.ContainsKey(seed))
                seedStates[seed] = Random.state;
            else seedStates.Add(seed, Random.state);
        }

        private static void useOrInitState(int seed)
        {
            Random.State state;

            if (seedStates.TryGetValue(seed, out state))
                Random.state = state;
            else Random.InitState(seed);
        }
    }
}
