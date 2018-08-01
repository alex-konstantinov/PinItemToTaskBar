using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinItemToTaskBar
{
    class Program
    {
        private static void PinUnpinTaskBar(string executablePath)
        {
            if (!File.Exists(executablePath)) throw new FileNotFoundException(executablePath);

            var tempVbsFile = Path.GetTempFileName().Replace(".tmp", ".vbs");
            File.WriteAllText(tempVbsFile, Resource.PinItem);

            var processOptions = new ProcessStartInfo("cscript", $"/nologo /b {tempVbsFile} \"{executablePath}\"")
            {
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(processOptions);
            process.WaitForExit();
        }

        static void Main(string[] args)
        {
            var assemblyPath = "c:\\windows\\";
            var pinExecutable = "notepad.exe";

            PinUnpinTaskBar(Path.Combine(Path.GetDirectoryName(assemblyPath), pinExecutable));
        }
    }
}
