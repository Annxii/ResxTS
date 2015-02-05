using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core.Scanner
{
    public class StringParseException : Exception
    {
        public StringParseException()
            : base()
        {

        }

        public StringParseException(string message)
            : base(message)
        {

        }
    }
}
