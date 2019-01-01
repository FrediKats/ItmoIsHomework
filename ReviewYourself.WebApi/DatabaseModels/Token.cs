using System;

namespace ReviewYourself.WebApi.DatabaseModels
{
    public class Token
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
    }
}