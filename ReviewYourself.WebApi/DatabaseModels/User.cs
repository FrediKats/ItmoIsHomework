using System;

namespace ReviewYourself.WebApi.DatabaseModels
{
    //TODO: add attributes
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        //TODO:
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
    }
}