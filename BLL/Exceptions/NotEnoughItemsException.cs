using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public  class NotEnoughItemsException : Exception
    {
        public NotEnoughItemsException() { }
        public NotEnoughItemsException(string message) : base(message) { }
    }
}
