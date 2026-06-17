using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace sdparm_GUI.Services;

public class SdparmProcessRunner
{
    public async Task<int> RunAsync(
        string executablePath,
        string arguments,
        Action<string> onOutputLine,
        CancellationToken cancellationToken)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = executablePath,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using var process = new Process { StartInfo = startInfo, EnableRaisingEvents = true };
        process.OutputDataReceived += (_, e) => { if (e.Data is not null) onOutputLine(e.Data); };
        process.ErrorDataReceived += (_, e) => { if (e.Data is not null) onOutputLine(e.Data); };

        try
        {
            process.Start();
        }
        catch (Win32Exception ex)
        {
            onOutputLine($"Failed to start '{executablePath}': {ex.Message}");
            onOutputLine("Check the sdparm executable path is correct and on your PATH.");
            return -1;
        }

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        await using var registration = cancellationToken.Register(() =>
        {
            try
            {
                if (!process.HasExited)
                    process.Kill(entireProcessTree: true);
            }
            catch (InvalidOperationException)
            {
                // Process already exited between the check and the kill.
            }
        });

        await process.WaitForExitAsync(CancellationToken.None);
        return process.ExitCode;
    }
}
