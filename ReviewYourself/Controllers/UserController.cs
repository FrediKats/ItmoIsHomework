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
    public class UserController : ApiController
    {
        // Post: Sign-in
        [HttpPost]
        [Route("Sign-in")]
        public Token SignIn([FromBody]AuthorizeData authData)
        {
            throw new NotImplementedException();
        }

        // Post: Sign-out
        [HttpPost]
        [Route("Sign-out")]
        public void SignOut([FromBody]Token token)
        {
            throw new NotImplementedException();
        }

        // Post: Sign-up
        [HttpPost]
        [Route("sign-up")]
        public void SignUp([FromBody]RegistrationData registrationData)
        {
            throw new NotImplementedException();
        }

        // GET: GetUser/{id}
        [HttpGet]
        [Route("GetUser/{id}")]
        public ResourceUser GetUser([FromUri]Guid userId)
        {
            throw new NotImplementedException();
        }

        // GET: FindUser/{username}
        [HttpGet]
        [Route("FindUser/{username}")]
        public ResourceUser FindUserByUsername([FromUri]string username)
        {
            throw new NotImplementedException();
        }

        // POST: Update/{token}
        [HttpPost]
        [Route("Update/{token}")]
        public void UpdateUser([FromUri]Token token, [FromBody]ResourceUser user)
        {
            throw new NotImplementedException();
        }
    }
}
