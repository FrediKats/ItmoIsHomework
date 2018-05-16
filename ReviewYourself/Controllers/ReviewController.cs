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
            _reviewService.CreateReview(review, token);
        }

        [HttpGet]
        [Route("{reviewId}")]
        public Review Get(Guid reviewId, [FromUri]Token token)
        {
            return _reviewService.GetReview(reviewId, token);
        }

        [HttpGet]
        [Route("GetBySolution/{solutionId}")]
        public IEnumerable<Review> GetBySolution(Guid solutionId, [FromUri]Token token)
        {
            return _reviewService.GetReviewBySolution(solutionId, token);
        }

        [HttpDelete]
        [Route("{reviewId}")]
        public void Delete(Guid reviewId, [FromUri]Token token)
        {
            _reviewService.DeleteReview(reviewId, token);
        }
    }
}
