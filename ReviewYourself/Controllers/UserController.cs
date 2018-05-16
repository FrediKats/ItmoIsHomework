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
            _userService.SignUp(registrationData.Login, registrationData.Password, new ResourceUser()
            {
                FirstName = registrationData.FirstName,
                Login = registrationData.Login,
                LastName = registrationData.LastName
            });
        }

        [HttpGet]
        public ResourceUser GetUser([FromUri]Token token)
        {
            return _userService.GetUser(token);
        }

        [HttpGet]
        [Route("{id}")]
        public ResourceUser GetUser(Guid userId,[FromUri]Token token)
        {
            return _userService.GetUser(userId, token);
        }

        [HttpGet]
        [Route("FindByUsername/{username}")]
        public ResourceUser FindByUsername(string username, [FromUri]Token token)
        {
            return _userService.FindUserByUsername(username, token);
        }

        [HttpPost]
        [Route("Update/")]
        public void UpdateUser([FromUri]Token token, [FromBody]ResourceUser user)
        {
            _userService.UpdateUser(user, token);
        }
    }
}
