using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace DotNetToolkit
{
    public static class CmdHelper
    {
        public static string Run(string fileName, string arguments)
        {
            Console.WriteLine($"{fileName} {arguments}");

            ProcessStartInfo procStartInfo = new ProcessStartInfo(fileName);
            procStartInfo.Arguments = arguments;
            // The following commands are needed to redirect the standard output.
            // This means that it will be redirected to the Process.StandardOutput StreamReader.
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            // Do not create the black window.
            procStartInfo.CreateNoWindow = true;
            if (procStartInfo.EnvironmentVariables["OS"] == "Windows_NT")
            {
                procStartInfo.FileName = fileName;
            }
            else
            {
                procStartInfo.FileName = "sh";
            }

            Process proc = new Process();
            proc.StartInfo = procStartInfo;
            proc.Start();

            string buffer = String.Empty;
            string output = String.Empty;

            while (!proc.HasExited)
            {
                Thread.Sleep(1);
                buffer = proc.StandardOutput.ReadLine();
                output += buffer;
                Console.WriteLine(buffer);
            }

            return output;
        }
    }
}
