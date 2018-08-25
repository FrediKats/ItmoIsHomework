using System;
using Microsoft.AspNetCore.Mvc;
using ReviewYourself.WebApi.DatabaseModels;
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

        [HttpGet("{id}")]
        public ActionResult<User> Get(Guid id)
        {
            return _userService.Get(id);
        }

        [HttpGet("Find/{username}")]
        public ActionResult<User> Get(string username)
        {
            return _userService.Get(username);
        }

        [HttpPost("Update")]
        public void Update([FromBody] User user, [FromRoute] Token token)
        {
            _userService.Update(user, token.UserId);
        }

        [HttpGet("Delete/{id}")]
        public void Delete(Guid id, [FromRoute] Token token)
        {
            _userService.Disable(id, token.UserId);
        }
    }
}