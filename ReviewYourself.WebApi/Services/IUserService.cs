using System;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Services
{
    public interface IUserService
    {
        User GetUser(Guid userId);
        User GetUser(string username);
        void UpdateUser(User user, Guid executorId);
        void DisableUser(Guid userId, Guid executorId);
    }
}