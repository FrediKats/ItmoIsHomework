using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Repositories
{
    public interface IUserRepository
    {
        void Create(ResourceUser user);
        ResourceUser Read(Guid id);
        ResourceUser ReadByUserName(string username);
        void Update(ResourceUser user);
        void Delete(ResourceUser user);
    }
}