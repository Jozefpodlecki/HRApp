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

        public PeopleController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetForPersonAsync(Guid personId, int top = 10, int offset = 0)
        {
            var result = await _personRepository.GetForPersonIdAsync(personId, top, offset);

            return Ok(result);
        }
    }
}
