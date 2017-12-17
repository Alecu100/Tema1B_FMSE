using System;

namespace Tema1B_FMSE.SyntaxNodes
{
    public class SyntaxTreeParserException : Exception
    {
        public SyntaxTreeParserException(string message, int index) : base(message + " at:" + index)
        {
        }
    }
}