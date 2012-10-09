using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XHost.Commands;

namespace XHost.Plugins.Fire
{
    public class FireCommand : ICommand
    {
        public string Name
        {
            get { return "fire"; }
        }

        public string ShortName
        {
            get { return null; }
        }

        public string Description
        {
            get { return "Add host entries for some well-known blocked websites."; }
        }

        public string Usage
        {
            get { return "xhost -" + Name; }
        }

        public void Execute(CommandLine commandLine, CommandExecutionContext context)
        {
            var entries = new Dictionary<string, string>
            {
                { "157.56.8.150", "nuget.org" },
                { "216.121.112.229", "ayende.com" },
                { "74.125.91.113", "groups.google.com" }
            };

            foreach (var entry in entries)
            {
                context.Hosts.Set(entry.Key, entry.Value);
            }

            context.Hosts.Save();

            Console.WriteLine("OK");
        }
    }
}
