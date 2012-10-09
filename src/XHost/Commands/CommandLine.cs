using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace XHost.Commands
{
    public class CommandLine
    {
        public string CommandName { get; set; }

        public IList<string> Parameters { get; private set; }

        public CommandOptionCollection Options { get; private set; }

        public CommandLine(string commandName)
        {
            if (String.IsNullOrEmpty(commandName))
                throw new ArgumentException("Command name is required.", "commandName");

            CommandName = commandName;
            Parameters = new List<string>();
            Options = new CommandOptionCollection();
        }

        public static CommandLine Parse(string commandLine)
        {
            if (String.IsNullOrEmpty(commandLine)) return null;

            var args = commandLine.Split(' ')
                                  .Select(x => x.Trim())
                                  .Where(x => !String.IsNullOrEmpty(x))
                                  .ToList();

            if (args.Count == 0) return null;

            if (args[0] == "-")
                throw new InvalidOperationException("Single - encountered. Command name should be the first argument and should start with - char");

            var cmdLine = new CommandLine(args[0].Substring(1));

            for (var i = 1; i < args.Count; i++)
            {
                var arg = args[i];

                if (arg.StartsWith("-"))
                {
                    cmdLine.Options = CommandOptionCollection.Parse(args.Skip(i).ToList());
                    break;
                }
                else
                {
                    cmdLine.Parameters.Add(arg);
                }
            }

            return cmdLine;
        }
    }
}
