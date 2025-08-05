using System.Collections.Generic;
using System.Linq;

namespace SammanCoaching.ParameterizedTests.Tests.Concept
{
    public static class PrimeNumberUpTo1000Generator
    {
        public static bool IsPrime(int number)
        {
            if (number <= 2) return false;
            for (int i = 2; i <= number / 2; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        public static IEnumerable<int> GetPrimesUpTo1000()
        {
            return Enumerable.Range(0, 1000).Where(IsPrime);
        }
    }
}
