using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XHost.Commands;

namespace XHost
{
    class Program
    {
        static IOutput Output = DefaultOutput.Instance;

        static void Main(string[] args)
        {
            ExecuteWithExceptionHandling(() =>
            {
                CommandExecutors.Register(typeof(Program).Assembly);
            });

            if (args != null && args.Length > 0)
            {
                if (args[0].StartsWith("-") && args[0].Length > 1)
                {
                    args[0] = args[0].Substring(1);
                }

                ExecuteWithExceptionHandling(() => Run(String.Join(" ", args)));
            }
            else
            {
                Welcome();

                var command = Console.ReadLine();
                while (true)
                {
                    command = command.TrimStart('-');

                    if (!String.IsNullOrEmpty(command))
                    {
                        ExecuteWithExceptionHandling(() => Run(command));
                    }

                    command = Console.ReadLine();
                }
            }
        }

        static void Welcome()
        {
            Console.WriteLine(
                @"
XHost
A command line tool to manage hosts file
----------------------------------------
Copyright (C) SYM http://www.mouhong.me
----------------------------------------
");
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
            }
        }

        static void Run(string command)
        {
            var console = DefaultOutput.Instance;
            var cmd = Command.Parse(command);
            var executor = CommandExecutors.Find(cmd.Name);

            if (executor == null)
                throw new InvalidOperationException("Unrecognized command: " + cmd.Name);

            var context = new CommandExecutionContext(HostFile.Load(), console);

            executor.Execute(cmd, context);

            console.WriteLine();
        }
    }
}
