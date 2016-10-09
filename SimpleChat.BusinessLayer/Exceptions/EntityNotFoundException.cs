using System;

namespace SimpleChat.BusinessLayer.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Exception e) : base(null, e)
        {
        }
    }
}
