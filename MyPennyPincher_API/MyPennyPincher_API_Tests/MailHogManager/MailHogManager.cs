using System.Diagnostics;

namespace MyPennyPincher_API_Tests.MailhogManager;

public class MailHogManager
{
    private readonly string _fileName = "docker";
    private readonly string _startCommandArgs = "run -d --name mailhog -p 1025:1025 -p 8025:8025 mailhog/mailhog";
    private readonly string _stopCommandArgs = "rm -f mailhog";

    public void StartMailHog()
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = _fileName,
            Arguments = _startCommandArgs,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = startInfo };
        process.Start();

        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            RemoveAllMailHogContainers();
            throw new Exception($"Docker command failed: {error}");
        }
    }

    public void RemoveAllMailHogContainers()
    {
        var stopCommand = new ProcessStartInfo
        {
            FileName = _fileName,
            Arguments = _stopCommandArgs,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = stopCommand };
        process.Start();
        process.WaitForExit();
    }
}
