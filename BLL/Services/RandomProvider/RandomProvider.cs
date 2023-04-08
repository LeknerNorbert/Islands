using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.RandomProvider
{
    public class RandomProvider : IRandomProvider
    {
        private readonly Random random = new();

        public int GetRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
