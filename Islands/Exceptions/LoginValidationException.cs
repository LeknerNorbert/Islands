using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Islands.Exceptions
{
    public class LoginValidationException : Exception
    {
        public LoginValidationException() { }
        public LoginValidationException(string message) : base(message) { }
    }
}
