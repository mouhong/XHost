using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XHost.Commands
{
    public static class CommandFactory
    {
        static readonly List<ICommand> _commands = new List<ICommand>();

        public static IEnumerable<ICommand> All
        {
            get
            {
                return _commands;
            }
        }

        public static void Clear()
        {
            _commands.Clear();
        }

        public static void Register(Assembly assembly)
        {
            var interfaceType = typeof(ICommand);

            var types = from type in assembly.GetExportedTypes()
                        where type.IsClass && !type.IsAbstract && interfaceType.IsAssignableFrom(type)
                        select type;

            foreach (var type in types)
            {
                if (!_commands.Any(x => x.GetType() == type))
                {
                    _commands.Add((ICommand)Activator.CreateInstance(type));
                }
            }
        }

        public static void Register(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                Register(assembly);
            }
        }

        public static ICommand Find(string commandName)
        {
            return _commands.FirstOrDefault(x => 
                x.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase) 
                || (x.ShortName != null && x.ShortName.Equals(commandName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
