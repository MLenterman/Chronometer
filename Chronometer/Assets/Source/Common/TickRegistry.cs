using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chronometer
{
    public class TickRegistry
    {
        private List<ITickable> allTickables;
        private List<ITickable> toRegister;
        private List<ITickable> toDeregister;

        private TickType tickType; 

        public TickRegistry(TickType tickType)
        {
            allTickables = new List<ITickable>();
            toRegister = new List<ITickable>();
            toDeregister = new List<ITickable>();
            
            // TODO: Check for only one TickType
            this.tickType = tickType;
        }

        public void Reset()
        {
            allTickables.Clear();
            toRegister.Clear();
            toDeregister.Clear();
        }

        public void Register(ITickable tickable)
        {
            Debug.Assert(tickable == null, 
                "Trying to register a tickable that is null!");

            toRegister.Add(tickable);
        }

        public void Register(List<ITickable> tickables)
        {
            Debug.Assert(tickables == null | tickables.Count == 0, 
                "Trying to register a tickables list that is null or empty!");

            toRegister.AddRange(tickables);
        }

        public void DeRegister(ITickable tickable)
        {
            Debug.Assert(tickable == null, 
                "Trying to deregister a tickable that is null!");

            toDeregister.Add(tickable);
        }

        public void DeRegister(List<ITickable> tickables)
        {
            Debug.Assert(tickables == null | tickables.Count == 0, 
                "Trying to deregister a tickables list that is null or empty!");

            toDeregister.AddRange(tickables);
        }

        public void Tick()
        {
            allTickables.AddRange(toRegister);
            toRegister.Clear();

            foreach (ITickable tickable in toDeregister)
                allTickables.Remove(tickable);
            toDeregister.Clear();

            switch (tickType)
            {
                case TickType.Normal: 
                    callNormalTick();
                    break;
                case TickType.Short:
                    callShortTick();
                    break;
                case TickType.Medium:
                    callMediumTick();
                    break;
                case TickType.Long:
                    callLongTick();
                    break;
                case TickType.Rare:
                    callLongTick();
                    break;
            }   
        }

        public TickType GetTickType()
        {
            return tickType;
        }

        private void callNormalTick()
        {
            foreach(ITickable tickable in allTickables)
                tickable.OnNormalTick();
        }

        private void callShortTick()
        {
            foreach (ITickable tickable in allTickables)
                tickable.OnShortTick();
        }

        private void callMediumTick()
        {
            foreach (ITickable tickable in allTickables)
                tickable.OnMediumTick();
        }

        private void callLongTick()
        {
            foreach (ITickable tickable in allTickables)
                tickable.OnLongTick();
        }

        private void callRareTick()
        {
            foreach (ITickable tickable in allTickables)
                tickable.OnRareTick();
        }
    }
}
