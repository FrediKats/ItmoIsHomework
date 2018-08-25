using System;
using System.Linq;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Exceptions;
using ReviewYourself.WebApi.Tools;

namespace ReviewYourself.WebApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly PeerReviewContext _context;

        public UserService(PeerReviewContext context)
        {
            _context = context;
        }

        public User Get(Guid userId)
        {
            return _context.Users.Find(userId);
        }

        public User Get(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Login == username);
        }

        public void Update(User user, Guid executorId)
        {
            //TODO: check if executorId is admin
            if (user.Id != executorId)
            {
                throw new PermissionDeniedException(executorId, "On UserService.Update");
            }

            //TODO: validate fields
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Disable(Guid userId, Guid executorId)
        {
            throw new NotImplementedException();
        }
    }
}