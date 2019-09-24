using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizV2.Util
{
    public static class Randomizer
    {
        private static Random random;
        public static T[] Randomize<T>(this T[] array)
        {
            random = new Random();
            for(int i = array.Length - 1; i >= 0; i--)
            {
                int j = random.Next(i);
                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
            return array;
        }
    }
}
