using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace XHost.Commands
{
    public class Command
    {
        public string Name { get; set; }

        public IList<string> Parameters { get; private set; }

        public CommandOptionCollection Options { get; private set; }

        public Command(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("Command name is required.", "name");

            Name = name;
            Parameters = new List<string>();
            Options = new CommandOptionCollection();
        }

        public static Command Parse(string command)
        {
            if (String.IsNullOrEmpty(command)) return null;

            var parts = command.Split(' ')
                                   .Select(x => x.Trim())
                                   .Where(x => !String.IsNullOrEmpty(x))
                                   .ToList();

            if (parts.Count == 0) return null;

            var cmd = new Command(parts[0]);

            for (var i = 1; i < parts.Count; i++)
            {
                var arg = parts[i];

                if (arg.StartsWith("-"))
                {
                    cmd.Options = CommandOptionCollection.Parse(parts.Skip(i - 1).ToList());
                    break;
                }
                else
                {
                    cmd.Parameters.Add(arg);
                }
            }

            return cmd;
        }
    }
}
