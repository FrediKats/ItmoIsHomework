using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("id")]
        public ActionResult<User> Get(Guid id)
        {
            return _userService.GetUser(id);
        }

        [HttpGet]
        public ActionResult<User> Get([FromBody]string username)
        {
            return _userService.GetUser(username);
        }

        [HttpPost]
        public void Update([FromBody] User user, [FromRoute]Token token)
        {
            _userService.UpdateUser(user, token.UserId);
        }

        [HttpPost("id")]
        public void Delete(Guid id, [FromRoute]Token token)
        {
            _userService.DisableUser(id, token.UserId);
        }
    }
}
