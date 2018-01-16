using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetToolkit
{
    public static class ArrayHelper
    {
        public static T GetRandom<T>(List<T> list)
        {
            int idx = new Random().Next(list.Count);
            return list[idx];
        }
    }
}
