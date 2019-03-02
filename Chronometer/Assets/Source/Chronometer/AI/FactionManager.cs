using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronometer
{
    public class FactionManager : ITickable
    {
        private List<Faction> allFactions = new List<Faction>();
        private Faction playerFaction;
        private Faction aiFaction;
        
        public IEnumerable<Faction> AllFactions
        {
            get { return allFactions.AsEnumerable(); }
        }

        public Faction PlayerFaction
        {
            get { return playerFaction; }
            set { playerFaction = value; }
        }

        public Faction AiFaction
        {
            get { return aiFaction; }
            set { aiFaction = value; }
        }

        public FactionManager()
        {
            
        }

        public void OnNormalTick()
        {
            throw new NotImplementedException();
        }

        public void OnShortTick()
        {
            throw new NotImplementedException();
        }

        public void OnMediumTick()
        {
            throw new NotImplementedException();
        }

        public void OnLongTick()
        {
            throw new NotImplementedException();
        }

        public void OnRareTick()
        {
            throw new NotImplementedException();
        }

        public TickType GetTickType()
        {
            return TickType.Normal & TickType.Medium;
        }
    }
}
