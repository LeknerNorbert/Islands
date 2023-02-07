using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class EmailValidationTokenExpiredException : Exception
    {
        public EmailValidationTokenExpiredException() { }
        public EmailValidationTokenExpiredException(string message) : base(message) { }
    }
}
