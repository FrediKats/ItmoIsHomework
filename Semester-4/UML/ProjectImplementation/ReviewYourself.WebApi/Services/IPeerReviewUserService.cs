using System;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Services
{
    public interface IPeerReviewUserService
    {
        PeerReviewUser Get(Guid userId);
        PeerReviewUser Get(string username);
        void Update(PeerReviewUser peerReviewUser, Guid executorId);
        void Disable(Guid userId, Guid executorId);
    }
}