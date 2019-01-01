using System;
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
        private readonly IPeerReviewUserService _peerReviewUserService;

        public UserController(IPeerReviewUserService peerReviewUserService)
        {
            _peerReviewUserService = peerReviewUserService;
        }

        [HttpGet("{id}")]
        public ActionResult<PeerReviewUser> Get(Guid id)
        {
            return _peerReviewUserService.Get(id);
        }

        [HttpGet("Find/{username}")]
        public ActionResult<PeerReviewUser> Get(string username)
        {
            return _peerReviewUserService.Get(username);
        }

        [HttpPost("Update")]
        public void Update([FromBody] PeerReviewUser peerReviewUser, [FromRoute] UserToken token)
        {
            _peerReviewUserService.Update(peerReviewUser, token.UserId);
        }

        [HttpGet("Delete/{id}")]
        public void Delete(Guid id, [FromRoute] UserToken token)
        {
            _peerReviewUserService.Disable(id, token.UserId);
        }
    }
}