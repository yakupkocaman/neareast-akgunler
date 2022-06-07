using System;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Extensions
{
    public static class ListExtensions
    {
        private static Random Rand = null;

        public static List<T> PickRandom<T>(this T[] values, int minCount, int maxCount)
        {
            if (Rand == null) Rand = new Random();

            var count = Rand.Next(minCount, maxCount);
            return values.PickRandom(count);
        }

        public static List<T> PickRandom<T>(this T[] values, int count)
        {
            if (Rand == null) Rand = new Random();

            if (count >= values.Length)
                count = values.Length - 1;

            int[] indexes = Enumerable.Range(0, values.Length).ToArray();

            List<T> results = new List<T>();
            
            for (int i = 0; i < count; i++)
            {
                int j = Rand.Next(i, values.Length);

                int temp = indexes[i];
                indexes[i] = indexes[j];
                indexes[j] = temp;

                results.Add(values[indexes[i]]);
            }

            return results;
        }
    }
}
