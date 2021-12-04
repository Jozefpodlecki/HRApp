using System;

namespace HRApp.Common
{
    internal class DefaultSystemClock : ISystemClock
    {
        DateTime ISystemClock.UtcNow => DateTime.UtcNow;
    }
}
