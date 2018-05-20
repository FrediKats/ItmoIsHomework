using System;
using System.Web.Http;
using ReviewYourself.Models;
using ReviewYourself.Models.Services;
using ReviewYourself.Models.Tools;

namespace ReviewYourself.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Sign-in")]
        public Token SignIn([FromBody] AuthorizeData authData)
        {
            return _userService.SignIn(authData.Login, authData.Password);
        }

        [HttpPost]
        [Route("Sign-out")]
        public void SignOut([FromBody] Token token)
        {
            _userService.SignOut(token);
        }

        [HttpPost]
        [Route("sign-up")]
        public void SignUp([FromBody] ResourceUser newUser)
        {
            _userService.SignUp(newUser);
        }

        [HttpGet]
        public ResourceUser GetUser([FromUri] Token token)
        {
            return _userService.GetUser(token, token.UserId);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public ResourceUser GetById(Guid userId, [FromUri] Token token)
        {
            return _userService.GetUser(token, userId);
        }

        [HttpGet]
        [Route("GetByUsername/{username}")]
        public ResourceUser GetByUsername(string username, [FromUri] Token token)
        {
            return _userService.GetUserByUsername(token, username);
        }

        [HttpPost]
        [Route("Update")]
        public void UpdateUser([FromUri] Token token, [FromBody] ResourceUser user)
        {
            _userService.UpdateUser(token, user);
        }
    }
}