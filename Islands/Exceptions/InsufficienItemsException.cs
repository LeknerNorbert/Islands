using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Islands.Exceptions
{
    public  class InsufficientItemsException : Exception
    {
        public InsufficientItemsException() { }
        public InsufficientItemsException(string message) : base(message) { }
        public InsufficientItemsException(string message, Exception inner) : base(message, inner) { }
    }
}
