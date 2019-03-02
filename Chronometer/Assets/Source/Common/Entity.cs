using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronometer
{
    public class Entity : ITickable
    {
        public TickType TickType;

        public Entity()
        {
            TickType = TickType.Normal | TickType.Rare;
        }

        public TickType GetTickType()
        {
            return TickType;
        }

        public void OnLongTick()
        {
            throw new NotImplementedException();
        }

        public void OnMediumTick()
        {
            throw new NotImplementedException();
        }

        public void OnNormalTick()
        {
            throw new NotImplementedException();
        }

        public void OnRareTick()
        {
            throw new NotImplementedException();
        }

        public void OnShortTick()
        {
            throw new NotImplementedException();
        }
    }
}
