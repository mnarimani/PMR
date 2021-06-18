using System;

namespace PMR
{
    public class PoorManException : Exception
    {
        public PoorManException(string message) : base(message)
        {
        }
    }
}