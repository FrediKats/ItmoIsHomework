using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReviewYourself.Controllers
{
    public class ReviewController : ApiController
    {
        // GET: api/Review
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Review/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Review
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Review/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Review/5
        public void Delete(int id)
        {
        }
    }
}
