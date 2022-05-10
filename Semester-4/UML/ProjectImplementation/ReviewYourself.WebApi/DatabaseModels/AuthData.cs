using System.ComponentModel.DataAnnotations;
using ReviewYourself.WebApi.Models;

namespace ReviewYourself.WebApi.DatabaseModels
{
    public class AuthData
    {
        [Key]
        public string Login { get; set; }
        public string Password { get; set; }
    }
}