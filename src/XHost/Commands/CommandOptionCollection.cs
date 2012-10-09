using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands
{
    public class CommandOptionCollection : IEnumerable<CommandOption>
    {
        private IList<CommandOption> _arguments;

        public int Count
        {
            get
            {
                return _arguments.Count;
            }
        }

        public CommandOption this[int index]
        {
            get
            {
                return _arguments[index];
            }
        }

        public CommandOptionCollection()
        {
            _arguments = new List<CommandOption>();
        }

        public CommandOptionCollection(IEnumerable<CommandOption> arguments)
        {
            _arguments = new List<CommandOption>(arguments);
        }

        public CommandOption Find(string argumentName)
        {
            return _arguments.FirstOrDefault(x => x.Name.Equals(argumentName, StringComparison.OrdinalIgnoreCase));
        }

        public static CommandOptionCollection Parse(IList<string> args)
        {
            if (args == null || args.Count == 0)
            {
                return new CommandOptionCollection();
            }

            var arguments = new List<CommandOption>();

            CommandOption current = null;

            for (var i = 0; i < args.Count; i++)
            {
                var arg = args[i];

                if (arg == "-")
                        throw new InvalidOperationException("Single - encountered. Are you missing argument name?");

                if (arg[0] == '-')
                {
                    current = new CommandOption(arg.Substring(1));
                    arguments.Add(current);
                }
                else
                {
                    if (current == null)
                        throw new InvalidOperationException("Argument value should always follow a argument name. Input arg index: " + i);

                    current.Values.Add(arg);
                }
            }

            return new CommandOptionCollection(arguments);
        }

        public IEnumerator<CommandOption> GetEnumerator()
        {
            return _arguments.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
