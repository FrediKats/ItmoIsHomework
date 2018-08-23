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

        [HttpPost("Register")]
        public ActionResult Register([FromBody] RegistrationData data)
        {
            _authService.RegisterMember(data.Login, data.Password);
            throw new NotImplementedException();
        }

        [HttpGet("Logout")]
        public ActionResult Logout([FromRoute]Token token)
        {
            throw new NotImplementedException();
        }

        [HttpPost("Login")]
        public ActionResult<Token> Login([FromBody] AuthorizeData data)
        {
            throw new NotImplementedException();
        }

        [HttpGet("CheckUsername")]
        public ActionResult<bool> IsUsernameAvaliable([FromRoute] string username)
        {
            return _authService.IsUsernameAvaliable(username);
        }
    }
}