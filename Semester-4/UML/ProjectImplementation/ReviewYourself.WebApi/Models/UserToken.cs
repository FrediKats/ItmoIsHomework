using System;

namespace ReviewYourself.WebApi.Models
{
    public class UserToken
    {
        public string AccessToken { get; set; }
        public Guid UserId { get; set; }
    }
}