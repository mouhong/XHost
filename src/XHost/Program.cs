using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XHost.Commands;
using XHost.Commands.Impl;

namespace XHost
{
    class Program
    {
        static IOutput Output = DefaultOutput.Instance;

        static void Main(string[] args)
        {
            ExecuteWithExceptionHandling(() =>
            {
                CommandFactory.RegisterCommands(typeof(Program).Assembly);
            });

            if (args != null && args.Length > 0)
            {
                ExecuteWithExceptionHandling(() => Run(String.Join(" ", args)));
            }
            else
            {
                new HelpCommand().Execute(new CommandLine("help"), new CommandExecutionContext(null, Output));
            }
        }

        static void ExecuteWithExceptionHandling(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Output.ErrorLine(ex.Message);
                Output.ErrorLine(ex.StackTrace);
            }
        }

        static void Run(string commandLine)
        {
            var console = DefaultOutput.Instance;
            var cmdLine = CommandLine.Parse(commandLine);
            var command = CommandFactory.Find(cmdLine.CommandName);

            if (command == null)
                throw new InvalidOperationException("Unrecognized command: " + cmdLine.CommandName);

            var context = new CommandExecutionContext(HostFile.Load(), console);

            command.Execute(cmdLine, context);
        }
    }
}
