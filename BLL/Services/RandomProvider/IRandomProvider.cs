using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.RandomProvider
{
    public interface IRandomProvider
    {
        int GetRandomNumber(int min, int max);
    }
}
