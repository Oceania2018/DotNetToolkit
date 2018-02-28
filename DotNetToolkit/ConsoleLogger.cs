using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetToolkit
{
    public static class ConsoleLogger
    {
        public static void WriteLine(string from, string content)
        {
            Console.WriteLine($"{DateTime.UtcNow.ToLongTimeString()}");
            Console.WriteLine($"{from}: {content}");
        }
    }
}
