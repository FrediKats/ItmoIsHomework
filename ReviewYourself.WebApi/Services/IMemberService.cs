using System;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Services
{
    public interface IMemberService
    {
        User GetMember(Guid memberId);
        User GetMember(string username);
        void UpdateUser(User user, Guid executorId);
        void DisableUser(User user, Guid executorId);
    }
}