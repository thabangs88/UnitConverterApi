using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UnitConverterAPI.Interface;
using UnitConverterAPI.Model.Request;
using UnitConverterAPI.Model.Response;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace UnitConverterAPI.Controllers
{
    [ApiExplorerSettings(GroupName = "Authenticate")]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
      //  private readonly IAuthorizationsService _authorizationService;
        private readonly IAuthorizationsService _authorizationsService;

        public AuthenticationController(IAuthorizationsService authorizationsService)
        {
            _authorizationsService = authorizationsService;
        }

        /// <summary>
        /// Creates a Login Session for a specific User and a Specific Login Type
        /// </summary>

        [HttpPost("[action]")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Token([FromBody] TokenRequest request)
        {
            var token = _authorizationsService.Token(request).Result;

            if (token.response.Success)
                return Ok(token.model);
            else
                return StatusCode(200, token.response.ErrorMessage);
        }


        [HttpGet("test")]
        public async Task<IActionResult> test()
        {
            var token = await _authorizationsService.test();

            return Ok(token);

        }
    }
}
