using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogForU.Core.Exceptions
{
    public class EmptyFileNameException : Exception
    {
        private const string DefaultMessage = "File Name cannot be null or whitespace";
        public EmptyFileNameException() : base(DefaultMessage)
        {

        }
        public EmptyFileNameException(string message) : base(message)
        {

        }
    }
}
