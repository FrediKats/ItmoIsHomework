using System;
using System.Linq;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Exceptions;
using ReviewYourself.WebApi.Tools;

namespace ReviewYourself.WebApi.Services.Implementations
{
    public class PeerReviewUserService : IPeerReviewUserService
    {
        private readonly PeerReviewContext _context;

        public PeerReviewUserService(PeerReviewContext context)
        {
            _context = context;
        }

        public PeerReviewUser Get(Guid userId)
        {
            return _context.Users.Find(userId);
        }

        public PeerReviewUser Get(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Login == username);
        }

        public void Update(PeerReviewUser peerReviewUser, Guid executorId)
        {
            //TODO: check if executorId is admin
            if (peerReviewUser.Id != executorId)
            {
                throw new PermissionDeniedException(executorId, "On PeerReviewUserService.Update");
            }

            //TODO: validate fields
            _context.Users.Update(peerReviewUser);
            _context.SaveChanges();
        }

        public void Disable(Guid userId, Guid executorId)
        {
            throw new NotImplementedException();
        }
    }
}