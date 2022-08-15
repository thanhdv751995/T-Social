using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Randoms
{
    public class RandomAppService : ManagementAppService
    {
        private readonly Random random;
        public RandomAppService()
        {
            random = new();
        }

        public int RandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public double RandomDouble()
        {
            return random.NextDouble();
        }

        public int RandomTime(int totalCount)
        {
            decimal time = (decimal)totalCount / 10;

            double randomDouble = RandomDouble();

            while (randomDouble < 0.5)
            {
                randomDouble = RandomDouble();
            }

            int timeRandom = (int)Math.Ceiling(time * (decimal)randomDouble);

            return timeRandom;
        }

        public int RandomIndex(bool condition, int min, int max)
        {
            int randomIndex = RandomInt(min, max);

            while (condition)
            {
                randomIndex = RandomInt(min, max);
            }

            return randomIndex;
        }
    }
}
