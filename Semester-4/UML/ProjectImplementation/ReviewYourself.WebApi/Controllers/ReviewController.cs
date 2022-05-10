using System;
using Microsoft.AspNetCore.Mvc;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.WebApi.Controllers
{
    [Route("api/Review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("Create")]
        public ActionResult Create([FromBody] Review review)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _reviewService.Create(review, userId);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<Review> Get(Guid id)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            return _reviewService.Get(id, userId);
        }

        [HttpGet("UserReview/{solutionId}/{userId}")]
        public ActionResult<Review> GetUserReview(Guid solutionId, Guid targetId)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            return _reviewService.GetReviewBySolutionAndUser(solutionId, targetId, userId);
        }

        [HttpGet("SolutionReview/{solutionId}")]
        public ActionResult<Review> GetSolutionReview(Guid solutionId)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            return Ok(_reviewService.GetReviewsBySolution(solutionId, userId));
        }

        [HttpGet("Delete/{id}")]
        public void Delete(Guid id)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _reviewService.Delete(id, userId);
        }
    }
}