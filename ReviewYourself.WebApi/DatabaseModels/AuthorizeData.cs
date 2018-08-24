using System.ComponentModel.DataAnnotations;

namespace ReviewYourself.WebApi.DatabaseModels
{
    public class AuthorizeData
    {
        [Key] public string Login { get; set; }

        public string Password { get; set; }
    }
}