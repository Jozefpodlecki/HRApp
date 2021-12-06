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
using RabbitMQ.Client.Core.DependencyInjection.Services;
using HRApp.Web.Messages;
using HRApp.DAL;
using HRApp.DAL.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        private readonly IQueueService _queueService;
        private bool _isRunning = false;

        public GenerateTestDataHostedService(
            ILogger<GenerateTestDataHostedService> logger,
            ISystemClock systemClock,
            IServiceScopeFactory serviceScopeFactory,
            ITaskManager taskManager,
            ITimer timer,
            IHubContext<HRAppHub> hubContext,
            IQueueService queueService)
        {
            _logger = logger;
            _systemClock = systemClock;
            _serviceScopeFactory = serviceScopeFactory;
            _taskManager = taskManager;
            _timer = timer;
            _hubContext = hubContext;
            _queueService = queueService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken = default)
        {
            try
            {
                stoppingToken.ThrowIfCancellationRequested();
                _timer.Tick += OnTick;
                _timer.Start(TimeSpan.FromSeconds(10), TimeSpan.FromMinutes(2));
                
                await _taskManager.Delay(Timeout.InfiniteTimeSpan, stoppingToken);
            } 
            catch (OperationCanceledException)
            {
                _timer.Stop();
            }
        }

        private static int Counter = 5;

        private async void OnTick(object sender, TimerEventArgs e)
        {
            if (_isRunning)
            {
                return;
            }

            _isRunning = true;

            try
            {
                using var serviceScope = _serviceScopeFactory.CreateScope();
                var serviceProvider = serviceScope.ServiceProvider;
                var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();

                var person = await appDbContext.People.FirstOrDefaultAsync();

                var annualLeaveApplication = new AnnualLeaveApplication
                {
                    CreatedById = person.Id,
                    CreatedOn = _systemClock.UtcNow,
                    Date = _systemClock.UtcNow.Date.AddDays(Counter),
                };
                Counter++;

                appDbContext.AnnualLeaveApplications.Add(annualLeaveApplication);
                await appDbContext.SaveChangesAsync();

                var message = new NewAnnualLeave
                {
                    ApplicationId = annualLeaveApplication.Id,
                    PersonId = person.Id,
                };

                await _queueService.SendAsync(message, NewAnnualLeave.ExchangeKey, NewAnnualLeave.RouteKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while generating test application");
            }

            _isRunning = false;
        }
    }
}
