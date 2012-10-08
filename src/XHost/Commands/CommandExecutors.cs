using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XHost.Commands
{
    public static class CommandExecutors
    {
        // Key: command name or short name; Value: command executor instance
        static readonly Dictionary<string, ICommandExecutor> _executors = new Dictionary<string, ICommandExecutor>(StringComparer.OrdinalIgnoreCase);

        public static void Register(Assembly assembly)
        {
            var interfaceType = typeof(ICommandExecutor);

            var types = from type in assembly.GetExportedTypes()
                        where type.IsClass && !type.IsAbstract && interfaceType.IsAssignableFrom(type)
                        select type;

            foreach (var type in types)
            {
                var command = (ICommandExecutor)Activator.CreateInstance(type);
                _executors.Add(command.Name, command);

                if (!String.IsNullOrEmpty(command.ShortName) 
                    && !command.ShortName.Equals(command.Name, StringComparison.OrdinalIgnoreCase))
                {
                    _executors.Add(command.ShortName, command);
                }
            }
        }

        public static ICommandExecutor Find(string commandName)
        {
            ICommandExecutor command;

            if (_executors.TryGetValue(commandName, out command))
            {
                return command;
            }

            return null;
        }
    }
}
