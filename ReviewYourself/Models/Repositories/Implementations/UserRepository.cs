using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public void Create(ResourceUser user)
        {
            throw new NotImplementedException();
        }

        public ResourceUser Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public ResourceUser ReadByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public ICollection<ResourceUser> ReadByCourse(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public void Update(ResourceUser user)
        {
            throw new NotImplementedException();
        }

        public void Delete(ResourceUser user)
        {
            throw new NotImplementedException();
        }
    }
}