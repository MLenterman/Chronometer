using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronometer
{
    public interface ITickable
    {
        void OnNormalTick();
        void OnShortTick();
        void OnMediumTick();
        void OnLongTick();
        void OnRareTick();

        TickType GetTickType();
    }
}
