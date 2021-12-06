using HRApp.Common;
using HRApp.DAL.Models;
using HRApp.DAL.Repositories;
using HRApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Core.DependencyInjection.Services;
using HRApp.Web.Messages;

namespace HRApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IOutOfOfficeRepository _outOfOfficeRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ISystemClock _systemClock;
        private readonly IUserContext _userContext;
        private readonly IQueueService _queueService;

        public ApplicationController(
            IApplicationRepository applicationRepository,
            IOutOfOfficeRepository outOfOfficeRepository,
            IPersonRepository personRepository,
            ISystemClock systemClock,
            IUserContext userContext,
            IQueueService queueService)
        {
            _applicationRepository = applicationRepository;
            _outOfOfficeRepository = outOfOfficeRepository;
            _personRepository = personRepository;
            _systemClock = systemClock;
            _userContext = userContext;
            _queueService = queueService;
        }

        [HttpGet("assigned")]
        public async Task<IActionResult> GetForPersonAsync(
            [FromQuery] int top = 10,
            [FromQuery] int offset = 0)
        {
            var personId = _userContext.UserId.Value;
            var result = await _applicationRepository.GetForPersonAsync(personId, top, offset);

            return Ok(result);
        }

        [HttpPost("annual-leave")]
        public async Task<IActionResult> CreateAnnualLeaveApplication(NewAnnualLeaveApplication model)
        {
            var userId = _userContext.UserId.Value;

            var application = new AnnualLeaveApplication
            {
                CreatedOn = _systemClock.UtcNow,
                CreatedById = userId,
                CreatedByName = _userContext.Email,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                Date = model.Date,
                Time = model.Time,
            };

            var result = await _applicationRepository.CreateAsync(application);

            var message = new NewAnnualLeave
            {
                ApplicationId = application.Id,
                PersonId = userId,
            };

            await _queueService.SendAsync(message, string.Empty, NewAnnualLeave.RouteKey);

            return Ok(result);
        }

        [HttpPost("annual-leave/accept/{id}")]
        public async Task<IActionResult> AcceptAnnualLeaveApplication(int id)
        {
            var result = await _applicationRepository.GetByIdAsync(id);

            if(result == null)
            {
                return NotFound();
            }

            var outofOffice = new OutOfOffice
            {

            };

            await _outOfOfficeRepository.CreateAsync(outofOffice);

            return Ok(result);
        }
    }
}
