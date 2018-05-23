using System;
using System.Web.Http;
using System.Web.Http.Description;
using ReviewYourself.Models;
using ReviewYourself.Models.Services;

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
        [ResponseType(typeof(void))]
        public IHttpActionResult Add([FromUri] Token token, [FromBody] Review review)
        {
            try
            {
                _reviewService.CreateReview(token, review);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(Review))]
        [Route("{reviewId}")]
        public IHttpActionResult Get(Guid reviewId, [FromUri] Token token)
        {
            try
            {
                var result = _reviewService.GetReview(token, reviewId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(Review[]))]
        [Route("GetBySolution/{solutionId}")]
        public IHttpActionResult GetBySolution(Guid solutionId, [FromUri] Token token)
        {
            try
            {
                var result = _reviewService.GetReviewListBySolution(token, solutionId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(Review))]
        [Route("GetBySolutionANdUser/{solutionId}/{userId}")]
        public IHttpActionResult GetBySolutionAndUser(Guid solutionId, Guid userId, [FromUri] Token token)
        {
            try
            {
                var result = _reviewService.GetReviewBySolutionAndUser(token, solutionId, userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [ResponseType(typeof(void))]
        [Route("{reviewId}")]
        public IHttpActionResult Delete(Guid reviewId, [FromUri] Token token)
        {
            try
            {
                _reviewService.DeleteReview(token, reviewId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}