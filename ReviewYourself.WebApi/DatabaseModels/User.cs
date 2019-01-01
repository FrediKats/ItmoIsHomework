using System;
using System.ComponentModel.DataAnnotations.Schema;
using ReviewYourself.WebApi.Models;

namespace ReviewYourself.WebApi.DatabaseModels
{
    //TODO: add attributes
    [Table("UsersTable")]
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public UserRole Role { get; set; }
    }
}