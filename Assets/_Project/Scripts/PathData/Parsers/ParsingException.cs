using System;

namespace PolSl.UrbanHealthPath
{
    public class ParsingException : Exception
    {
        public ParsingException() : base("Parsing exception!")
        {
        }

        public ParsingException(string message) : base(message)
        {
        }
    }
}