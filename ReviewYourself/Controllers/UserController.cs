using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public Token SignIn([FromBody]AuthorizeData authData)
        {
            return _userService.SignIn(authData.Login, authData.Password);
        }

        [HttpPost]
        [Route("Sign-out")]
        public void SignOut([FromBody]Token token)
        {
            _userService.SignOut(token);
        }

        [HttpPost]
        [Route("sign-up")]
        public void SignUp([FromBody]RegistrationData registrationData)
        {
            //TODO: add other data
            _userService.SignUp(registrationData.Login, registrationData.Password);
        }

        [HttpGet]
        [Route("{id}")]
        public ResourceUser GetUser(Guid userId)
        {
            //TODO: Token?
            return _userService.GetUser(userId);
        }

        [HttpGet]
        [Route("FindByUsername/{username}")]
        public ResourceUser FindByUsername(string username)
        {
            //TODO: Token?
            return _userService.FindUserByUsername(username);
        }

        [HttpPost]
        [Route("Update/")]
        public void UpdateUser([FromUri]Token token, [FromBody]ResourceUser user)
        {
            _userService.UpdateUser(user, token);
        }
    }
}
