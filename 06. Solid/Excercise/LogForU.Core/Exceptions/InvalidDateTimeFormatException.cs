using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogForU.Core.Exceptions
{
    public class InvalidDateTimeFormatException : Exception
    {

        private const string DefaultMessage = "Invalid DateTime Format";
        public InvalidDateTimeFormatException() : base(DefaultMessage)
        {

        }
        public InvalidDateTimeFormatException(string message) : base(message)
        {

        }

    }
}
