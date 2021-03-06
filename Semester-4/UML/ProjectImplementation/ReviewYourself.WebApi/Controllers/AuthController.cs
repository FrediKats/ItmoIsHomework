using Microsoft.AspNetCore.Mvc;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.WebApi.Controllers
{
    [Route("api/auth")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPeerReviewAuthService _authService;

        public AuthController(IPeerReviewAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public ActionResult Register([FromBody] RegistrationData data)
        {
            _authService.RegisterMember(data);
            return Ok();
        }

        [HttpGet("Logout")]
        public ActionResult Logout([FromRoute] UserToken token)
        {
            _authService.LogOut(token);
            return Ok();
        }

        [HttpPost("Login")]
        public ActionResult<UserToken> Login([FromBody] AuthData data)
        {
            return _authService.LogIn(data);
        }

        [HttpGet("CheckUsername")]
        public ActionResult<bool> IsUsernameAvailable([FromRoute] string username)
        {
            return _authService.IsUsernameAvailable(username);
        }
    }
}