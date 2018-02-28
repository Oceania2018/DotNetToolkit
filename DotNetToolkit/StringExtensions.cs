using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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

        public static string GetMd5Hash(this string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
    }
}
