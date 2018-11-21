using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    static class Tools
    {
        internal static T[][] CreateArray<T>(int n, int m)
        {
            var array = new T[n][];

            for (int i = 0; i < n; i++)
            {
                array[i] = new T[m];
            }

            return array;
        }
    }
}
