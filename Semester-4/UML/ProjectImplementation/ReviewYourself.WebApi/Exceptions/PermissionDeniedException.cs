using System;

namespace ReviewYourself.WebApi.Exceptions
{
    public class PermissionDeniedException : Exception
    {
        public PermissionDeniedException(Guid userId, string message = null)
            : base($"Permission denied for {userId}" + (message == null ? "" : $"\n{message}"))
        {
            
        }
    }
}