using GitHubAutogradingTests.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubAutogradingTests
{
    internal class GitHubAssignment
    {
        private readonly string _basePath;
        private readonly string _defaultStdOut;
        private readonly string[] _executableFileMasks;

        public GitHubAssignment(string basePath, string defaultStdOut, string[] executableFileMasks)
        {
            _basePath = basePath;
            _defaultStdOut = defaultStdOut;
            _executableFileMasks = executableFileMasks;
        }
        public string Execute(string param)
        {
            Debug.Print("Starting execution of Assignment");
            var executables = _executableFileMasks
                .SelectMany(mask => Directory.GetFiles(_basePath, mask, SearchOption.AllDirectories))
                .Select(x => Path.GetFullPath(x))
                .Distinct()
                .ToArray();
            Debug.Print($"Found {executables.Length} executables: {Environment.NewLine}{string.Join(Environment.NewLine, executables)}");

            if (!executables.Any())
            {
                throw new AssignmenthasNoExecutables($"No executables found for path {_basePath}");
            }

            Debug.Print($"Executing...");
            var results = executables.Select(executable => Execute(executable, param)).ToArray();
            Debug.Print($"Got {results.Length} results");

            Debug.Print($"Checking executed with errors...");
            var exitedWithError = results.Where(x => x.ExitCode != 0);
            if (exitedWithError.Any())
            {
                var messages = exitedWithError.Select(x => $"Path: {x.Path} Code: {x.ExitCode} StdErr: {x.StdErr} StdOut: {x.StdOut}");
                throw new AssignmentExecutionError($"Executed with errors: {Environment.NewLine}{string.Join(Environment.NewLine, messages)}");
            }
            Debug.Print($"Ok");

            Debug.Print($"Deleting from results ignored results");
            results = results.Where(res => !res.StdOut.Contains(_defaultStdOut)).ToArray();
            Debug.Print($"Got {results.Length} results");

            if (!results.Any())
            {
                throw new AssignmentNothingChangedException($"All executable's stdout contains {_defaultStdOut}"); 
            }

            if (results.Length != 1)
            {
                var messages = results.Select(x => $"Path: {x.Path} Code: {x.ExitCode} StdErr: {x.StdErr} StdOut: {x.StdOut}");
                throw new AssignmentMultipleOkImplementations($"Executed okay: { Environment.NewLine }{ string.Join(Environment.NewLine, messages)}");
            }

            return results.Single().StdOut.Trim();
        }

        private (string Path, int ExitCode, string StdOut, string StdErr) Execute(string program, string arguments)
        {
            var processStartInfo =
                new ProcessStartInfo
                {
                    FileName = "bash",
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    Arguments = $"{program} {arguments}"
                };

            using var process = new Process() { StartInfo = processStartInfo };
            process.Start();

            var executionStopwatch = new Stopwatch();
            executionStopwatch.Start();

            using var srdOutTask = Task.Run(process.StandardOutput.ReadToEnd);
            using var srdErrTask = Task.Run(process.StandardError.ReadToEnd);

            var exited = process.WaitForExit((int)TimeSpan.FromSeconds(30).TotalMilliseconds);

            if (!exited)
            {
                throw new AssignmentOutOfTimeException($"Program {program} executing more than 30 sec");
            }

            var output = srdOutTask.Result;
            var error = srdErrTask.Result;

            return (program, process.ExitCode, output, error);
        }

    }    
}
