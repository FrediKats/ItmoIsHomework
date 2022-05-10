using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewYourself.WebApi.DatabaseModels;
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

        [Authorize]
        [HttpPost("Update")]
        public void Update([FromBody] PeerReviewUser peerReviewUser)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _peerReviewUserService.Update(peerReviewUser, userId);
        }

        [HttpGet("Delete/{id}")]
        public void Delete(Guid id)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _peerReviewUserService.Disable(id, userId);
        }
    }
}