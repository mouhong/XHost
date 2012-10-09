using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using XHost.Commands;
using XHost.Commands.Impl;

namespace XHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteWithExceptionHandling(() =>
            {
                CommandFactory.Register(typeof(Program).Assembly);

                // load plugins
                var pluginsDirectory = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins"));
                if (pluginsDirectory.Exists)
                {
                    foreach (var dll in pluginsDirectory.GetFiles("*.dll", SearchOption.TopDirectoryOnly))
                    {
                        if ((dll.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;

                        CommandFactory.Register(Assembly.LoadFile(dll.FullName));
                    }
                }
            });

            if (args == null || args.Length == 0)
            {
                args = new[] { "-help" };
            }

            ExecuteWithExceptionHandling(() => Run(String.Join(" ", args)));
        }

        static void ExecuteWithExceptionHandling(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                ConsoleUtil.ErrorLine(ex.Message);
            }
        }

        static void Run(string commandLine)
        {
            var cmdLine = CommandLine.Parse(commandLine);
            var command = CommandFactory.Find(cmdLine.CommandName);

            if (command == null)
                throw new InvalidOperationException("Unknown command: " + cmdLine.CommandName);

            var context = new CommandExecutionContext(HostFile.Load());

            command.Execute(cmdLine, context);
        }
    }
}
