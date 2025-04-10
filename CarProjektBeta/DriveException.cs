using System;

namespace CarProjektBeta
{
    public class InvalidDistanceException : Exception
    {
        public InvalidDistanceException()
        {
        }

        public InvalidDistanceException(string message)
            : base(message)
        {
        }

        public InvalidDistanceException(string message, Exception inner)
            :base (message, inner)
        {
        }
    }
}

