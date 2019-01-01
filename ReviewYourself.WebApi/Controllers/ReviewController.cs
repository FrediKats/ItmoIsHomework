using System;
using Microsoft.AspNetCore.Mvc;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;
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
        public ActionResult Create([FromBody] Review review, [FromRoute] UserToken token)
        {
            _reviewService.Create(review, token.UserId);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<Review> Get(Guid id, [FromRoute] UserToken token)
        {
            return _reviewService.Get(id, token.UserId);
        }

        [HttpGet("UserReview/{solutionId}/{userId}")]
        public ActionResult<Review> GetUserReview(Guid solutionId, Guid userId, [FromRoute] UserToken token)
        {
            return _reviewService.GetReviewBySolutionAndUser(solutionId, userId, token.UserId);
        }

        [HttpGet("SolutionReview/{solutionId}")]
        public ActionResult<Review> GetSolutionReview(Guid solutionId, [FromRoute] UserToken token)
        {
            return Ok(_reviewService.GetReviewsBySolution(solutionId, token.UserId));
        }

        [HttpGet("Delete/{id}")]
        public void Delete(Guid id, [FromRoute] UserToken token)
        {
            _reviewService.Delete(id, token.UserId);
        }
    }
}