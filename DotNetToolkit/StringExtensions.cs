using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetToolkit
{
    public static class StringExtensions
    {
        public static string FirstCharacterToLower(this String str)
        {
            if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
            {
                return str;
            }

            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static string FirstCharacterToUpper(this String str)
        {
            if (String.IsNullOrEmpty(str) || Char.IsUpper(str, 0))
            {
                return str;
            }

            return Char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
