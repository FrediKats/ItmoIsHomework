using System;
using ReviewYourself.WebApi.Models;

namespace ReviewYourself.WebApi.DatabaseModels
{
    //TODO: add attributes
    public class PeerReviewUser
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