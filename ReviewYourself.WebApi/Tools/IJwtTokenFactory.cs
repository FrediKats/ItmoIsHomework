using System;

namespace ReviewYourself.WebApi.Tools
{
    public interface IJwtTokenFactory
    {
        string CreateJwtToken(Guid id);
    }
}