using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list, Random rnd)
        {
            int n = list.Count;
            while (n > 1)
            {
                int k = rnd.Next(n--);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}