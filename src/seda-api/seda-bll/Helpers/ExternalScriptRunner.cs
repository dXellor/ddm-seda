using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace seda_bll.Helpers;

public static class ExternalScriptRunner
{
    /// <summary>
    /// Runs script and returns std output
    /// </summary>
    /// <returns></returns>
    public static string RunPdfToTextScript(
        string pdfPath,
        [CallerFilePath] string? path = null )
    {
        var dInfo = new DirectoryInfo( path! );
        var sourceRoot = dInfo.Parent!.Parent!.Parent!.FullName;
        var scriptPathDirectory = Path.Combine( sourceRoot, "seda-pdf-parser" );
        const string cmd = "/usr/bin/bash";
        const string args = "";
        const string activateVenv = "source venv/bin/activate";
        var commandsToExecute = new List<string>(){
            $"venv/bin/python main.py {pdfPath}"
        };

        var startInfo = new ProcessStartInfo
        {
            RedirectStandardOutput = true,
            RedirectStandardInput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            Arguments = args,
            FileName = cmd,
            WorkingDirectory = scriptPathDirectory
        };

        var process = Process.Start(startInfo);
        if (process == null)
            throw new Exception("Could not start process");

        using var sw = process.StandardInput;
        if (sw.BaseStream.CanWrite)
        {
            sw.WriteLine(activateVenv);
            sw.Flush();
            foreach (var command in commandsToExecute)
            {
                sw.WriteLine(command);
            }
            sw.Flush();
            sw.Close();
        }

        var sb = new StringBuilder();
        while (!process.HasExited)
            sb.Append(process.StandardOutput.ReadToEnd());

        var error = process.StandardError.ReadToEnd();
        if (!string.IsNullOrEmpty(error))
            throw new Exception($"Something went wrong: \n{error}");

        return sb.ToString();
    }
}