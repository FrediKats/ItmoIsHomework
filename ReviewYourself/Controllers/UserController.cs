using System;
using System.Web.Http;
using System.Web.Http.Description;
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
        [ResponseType(typeof(Token))]
        [Route("Sign-in")]
        public IHttpActionResult SignIn([FromBody] AuthorizeData authData)
        {
            try
            {
                var result = _userService.SignIn(authData.Login, authData.Password);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("Sign-out")]
        public IHttpActionResult SignOut([FromBody] Token token)
        {
            try
            {
                _userService.SignOut(token);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("sign-up")]
        public IHttpActionResult SignUp([FromBody] ResourceUser newUser)
        {
            try
            {
                _userService.SignUp(newUser);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetUser([FromUri] Token token)
        {
            try
            {
                var result = _userService.GetUser(token, token.UserId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(ResourceUser))]
        [Route("GetById/{id}")]
        public IHttpActionResult GetById(Guid userId, [FromUri] Token token)
        {
            try
            {
                var result = _userService.GetUser(token, userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(ResourceUser))]
        [Route("GetByUsername/{username}")]
        public IHttpActionResult GetByUsername(string username, [FromUri] Token token)
        {
            try
            {
                var result = _userService.GetUserByUsername(token, username);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("Update")]
        public IHttpActionResult UpdateUser([FromUri] Token token, [FromBody] ResourceUser user)
        {
            try
            {
                _userService.UpdateUser(token, user);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}