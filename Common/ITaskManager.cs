﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace HRApp.Common
{
    public interface ITaskManager
    {
        Task Delay(TimeSpan delay);
        Task Delay(TimeSpan delay, CancellationToken cancellationToken);
    }
}
