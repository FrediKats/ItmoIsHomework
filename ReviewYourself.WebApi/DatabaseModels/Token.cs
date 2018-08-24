using System;

namespace ReviewYourself.WebApi.DatabaseModels
{
    public class Token
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
    }
}