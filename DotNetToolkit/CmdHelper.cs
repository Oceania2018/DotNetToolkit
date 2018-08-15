using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace DotNetToolkit
{
    public static class CmdHelper
    {
        public static string Run(string fileName, string arguments, bool outputAsync = true)
        {
            Console.WriteLine($"{fileName} {arguments}");

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            ProcessStartInfo procStartInfo = new ProcessStartInfo(fileName);
            procStartInfo.Arguments = arguments;
            // The following commands are needed to redirect the standard output.
            // This means that it will be redirected to the Process.StandardOutput StreamReader.
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.RedirectStandardError = true;
            procStartInfo.UseShellExecute = false;
            // Do not create the black window.
            //procStartInfo.CreateNoWindow = true;
            if (procStartInfo.EnvironmentVariables.ContainsKey("OS") && procStartInfo.EnvironmentVariables["OS"] == "Windows_NT")
            {
                procStartInfo.FileName = fileName + ".exe";
            }
            else
            {
                procStartInfo.FileName = "sh";
                procStartInfo.RedirectStandardInput = true;
                procStartInfo.CreateNoWindow = false;
            }
            proc.StartInfo = procStartInfo;

            string output = String.Empty;

            proc.Start();
            if (procStartInfo.EnvironmentVariables.ContainsKey("OS") && procStartInfo.EnvironmentVariables["OS"] == "Windows_NT")
            {
            
            }
            else
            {
                proc.StandardInput.WriteLine($"{fileName} {arguments}" + "&exit");
                proc.StandardInput.AutoFlush = false;
            }

            using (StreamReader reader = proc.StandardOutput)
            {
                if (outputAsync)
                {
                    string buffer = String.Empty;
                    while (!proc.HasExited)
                    {
                        Thread.Sleep(1);
                        buffer = proc.StandardOutput.ReadLine();
                        output += buffer;
                        Console.WriteLine(buffer);
                    }
                }
                else
                {
                    output = reader.ReadToEnd();
                    Console.WriteLine(output);
                }
            }
            
            proc.WaitForExit();
            proc.Close();

            return output;
        }
    }
}
