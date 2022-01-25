using System;

namespace PolSl.UrbanHealthPath
{
    public class ParsingException : Exception
    {
        public ParsingException() : base()
        {
        }

        public ParsingException(string message) : base(message)
        {
        }
    }
}