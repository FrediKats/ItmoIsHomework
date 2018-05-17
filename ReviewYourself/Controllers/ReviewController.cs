using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReviewYourself.Models;
using ReviewYourself.Models.Services;
using ReviewYourself.Models.Tools;

namespace ReviewYourself.Controllers
{
    [RoutePrefix("api/reviews")]
    public class ReviewController : ApiController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public void Add([FromUri]Token token, [FromBody]Review review)
        {
            _reviewService.CreateReview(token, review);
        }

        [HttpGet]
        [Route("{reviewId}")]
        public Review Get(Guid reviewId, [FromUri]Token token)
        {
            return _reviewService.GetReview(token, reviewId);
        }

        [HttpGet]
        [Route("GetBySolution/{solutionId}")]
        public IEnumerable<Review> GetBySolution(Guid solutionId, [FromUri]Token token)
        {
            return _reviewService.GetReviewBySolution(token, solutionId);
        }

        [HttpGet]
        [Route("GetBySolutionANdUser/{solutionId}/{userId}")]
        public Review GetBySolutionAndUser(Guid solutionId, Guid userId, [FromUri] Token token)
        {
            return _reviewService.GetReviewBySolutionAndUser(token, solutionId, userId);
        }

        [HttpDelete]
        [Route("{reviewId}")]
        public void Delete(Guid reviewId, [FromUri]Token token)
        {
            _reviewService.DeleteReview(token, reviewId);
        }
    }
}
