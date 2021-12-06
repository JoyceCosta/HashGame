using System;

namespace HashGame
{
    public class PlayException : Exception
    {
        // Constructor.
        public PlayException() { } 

        // Constructor. 
        public PlayException(string message) : base(message) { } 

        // Constructor. 
        public PlayException(string message, Exception inner) : base(message, inner) { }
    }
}