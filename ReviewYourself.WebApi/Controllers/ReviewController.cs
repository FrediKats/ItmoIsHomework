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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public ActionResult Create([FromBody]Review review, [FromRoute]Token token)
        {
            _reviewService.CreateReview(review, token.UserId);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<Review> Get(Guid id, [FromRoute]Token token)
        {
            return _reviewService.GetReview(id, token.UserId);
        }

        [HttpGet("UserReview/{solutionId}/{userId}")]
        public ActionResult<Review> GetUserReview(Guid solutionId, Guid userId, [FromRoute]Token token)
        {
            return _reviewService.GetReviewBySolutionAndUser(solutionId, userId, token.UserId);
        }

        [HttpGet("SolutionReview/{solutionId}")]
        public ActionResult<Review> GetSolutionReview(Guid solutionId, [FromRoute]Token token)
        {
            return Ok(_reviewService.GetReviewsBySolution(solutionId, token.UserId));
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id,[FromRoute]Token token)
        {
            _reviewService.DeleteReview(id, token.UserId);
        }
    }
}
