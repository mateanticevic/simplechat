using System;

namespace SimpleChat.BusinessLayer.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
        {

        }

        public UnauthorizedException(Exception e) : base(null, e)
        {

        }
    }
}
