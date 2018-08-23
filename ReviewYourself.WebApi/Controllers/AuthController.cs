using System;
using Microsoft.AspNetCore.Mvc;
using ReviewYourself.WebApi.Models;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.WebApi.Controllers
{
    [Route("api/auth")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthorizationService _authService;

        public AuthController(IAuthorizationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(void))]
        public IActionResult Register([FromBody] RegistrationData data)
        {
            _authService.RegisterMember(data.Login, data.Password);
            throw new NotImplementedException();
        }

        [HttpGet("logout/{id}")]
        [ProducesResponseType(200, Type = typeof(void))]
        public void Logout(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Token))]
        public ActionResult<Token> Login([FromBody] AuthorizeData data)
        {
            throw new NotImplementedException();
        }

        [HttpGet("check_username")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public ActionResult<bool> IsUsernameAvaliable([FromRoute] string username)
        {
            return _authService.IsUsernameAvaliable(username);
        }
    }
}