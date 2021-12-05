using HRApp.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HRApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;

        public UserController(
            IUserRepository userRepository,
            IUserContext userContext)
        {
            _userRepository = userRepository;
            _userContext = userContext;
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRolesForUserAsync()
        {
            var userId = _userContext.UserId.Value;
            var result = await _userRepository.GetRolesForUser(userId);

            return Ok(result);
        }
    }
}
