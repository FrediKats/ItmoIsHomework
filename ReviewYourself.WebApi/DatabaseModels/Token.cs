using System;

namespace ReviewYourself.WebApi.DatabaseModels
{
    public class Token
    {
        public PeerReviewUser PeerReviewUser { get; set; }
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
    }
}