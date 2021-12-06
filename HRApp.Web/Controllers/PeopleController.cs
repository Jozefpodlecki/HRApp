using HRApp.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUserContext _userContext;

        public PeopleController(
            IPersonRepository personRepository,
            IUserContext userContext)
        {
            _personRepository = personRepository;
            _userContext = userContext;
        }

        [HttpGet("members")]
        public async Task<IActionResult> GetForPersonAsync(
            [FromQuery] int top = 10,
            [FromQuery] int offset = 0)
        {
            var personId = _userContext.UserId.Value;
            var result = await _personRepository.GetForPersonIdAsync(personId, top, offset);

            return Ok(result);
        }
    }
}
