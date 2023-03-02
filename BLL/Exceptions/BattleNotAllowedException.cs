using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class BattleNotAllowedException : Exception
    {
        public BattleNotAllowedException() { }
        public BattleNotAllowedException(string message) : base(message) { }
        public BattleNotAllowedException(string message, Exception inner) : base(message, inner) { }
    }
}
