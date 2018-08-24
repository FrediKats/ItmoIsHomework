using System;

namespace ReviewYourself.WebApi.Models
{
    public class Token
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
    }
}