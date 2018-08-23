using System;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Services
{
    public interface IUserService
    {
        User Get(Guid userId);
        User Get(string username);
        void Update(User user, Guid executorId);
        void Disable(Guid userId, Guid executorId);
    }
}