using HRApp.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client.Core.DependencyInjection.MessageHandlers;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.Web.MessageHandlers
{
    public class NotifySignalRMessageHandler : IAsyncMessageHandler
    {
        private readonly IHubContext<HRAppHub> _hubContext;

        public NotifySignalRMessageHandler(IHubContext<HRAppHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task Handle(BasicDeliverEventArgs eventArgs, string matchingRoute)
        {
            return Task.CompletedTask;
        }
    }
}
