using System;

namespace RestaurantMVC.Exceptions
{
    public class ForbidException : Exception
    {
        public ForbidException(string message) : base(message)
        {
        }
    }
}
