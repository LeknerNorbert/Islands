using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class EmailNotValidatedException : Exception
    {
        public EmailNotValidatedException() { }
        public EmailNotValidatedException(string message) : base(message) { }
        public EmailNotValidatedException(string message, Exception inner) : base(message, inner) { }
    }
}
