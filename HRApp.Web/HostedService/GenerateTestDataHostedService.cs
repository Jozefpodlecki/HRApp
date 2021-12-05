using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using HRApp.Common;
using Microsoft.AspNetCore.SignalR;
using HRApp.Web.Hubs;

namespace HRApp.Web.HostedService
{
    internal class GenerateTestDataHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ISystemClock _systemClock;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ITaskManager _taskManager;
        private readonly ITimer _timer;
        private readonly IHubContext<HRAppHub> _hubContext;

        public GenerateTestDataHostedService(
            ILogger<GenerateTestDataHostedService> logger,
            ISystemClock systemClock,
            IServiceScopeFactory serviceScopeFactory,
            ITaskManager taskManager,
            ITimer timer,
            IHubContext<HRAppHub> hubContext)
        {
            _logger = logger;
            _systemClock = systemClock;
            _serviceScopeFactory = serviceScopeFactory;
            _taskManager = taskManager;
            _timer = timer;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken = default)
        {
            try
            {
                stoppingToken.ThrowIfCancellationRequested();
                _timer.Tick += OnTick;
                _timer.Start(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(30));
                
                await _taskManager.Delay(Timeout.InfiniteTimeSpan, stoppingToken);
            } 
            catch (OperationCanceledException)
            {
                _timer.Stop();
            }
        }

        private async void OnTick(object sender, TimerEventArgs e)
        {
            
        }
    }
}
