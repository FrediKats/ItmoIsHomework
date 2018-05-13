using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReviewYourself.Models;
using ReviewYourself.Models.Tools;

namespace ReviewYourself.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("Sign-in")]
        public Token SignIn([FromBody]AuthorizeData authData)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Sign-out")]
        public void SignOut([FromBody]Token token)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("sign-up")]
        public void SignUp([FromBody]RegistrationData registrationData)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetUser/{id}")]
        public ResourceUser GetUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("FindByUsername/{username}")]
        public ResourceUser FindByUsername(string username)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Update/")]
        public void UpdateUser([FromUri]Token token, [FromBody]ResourceUser user)
        {
            throw new NotImplementedException();
        }
    }
}
