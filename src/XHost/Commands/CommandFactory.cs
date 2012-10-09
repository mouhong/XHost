using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XHost.Commands
{
    public static class CommandFactory
    {
        static ICommand[] _commands;

        public static IEnumerable<ICommand> All
        {
            get
            {
                return _commands;
            }
        }

        public static void RegisterCommands(Assembly assemblyToScan)
        {
            var interfaceType = typeof(ICommand);

            var types = from type in assemblyToScan.GetExportedTypes()
                        where type.IsClass && !type.IsAbstract && interfaceType.IsAssignableFrom(type)
                        select type;

            _commands = types.Select(type => (ICommand)Activator.CreateInstance(type))
                             .OrderBy(x=>x.Name)
                             .ToArray();
        }

        public static ICommand Find(string commandName)
        {
            return _commands.FirstOrDefault(x => 
                x.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase) 
                || (x.ShortName != null && x.ShortName.Equals(commandName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
