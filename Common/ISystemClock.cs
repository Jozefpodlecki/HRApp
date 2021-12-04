using System;

namespace HRApp.Common
{
    public interface ISystemClock
    {
        DateTime UtcNow { get; }
    }
}
