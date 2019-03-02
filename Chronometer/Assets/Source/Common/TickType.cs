using System;

namespace Chronometer
{
    [Flags]
    public enum TickType : byte
    {
        None = 0,
        Normal = 1,
        Short = 2,
        Medium = 4,
        Long = 8,
        Rare = 16
    }
}
