using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XHost.Commands;

namespace XHost.Plugins.NuGet
{
    public class NuGetCommand : ICommand
    {
        public string Name
        {
            get { return "nuget"; }
        }

        public string ShortName
        {
            get { return null; }
        }

        public string Description
        {
            get { return "Add 157.56.8.150 nuget.org to hosts file."; }
        }

        public string Usage
        {
            get { return "xhost -nuget"; }
        }

        public void Execute(CommandLine commandLine, CommandExecutionContext context)
        {
            context.HostFile.Set("157.56.8.150", "nuget.org");
            context.HostFile.Save();

            Console.WriteLine("OK");
        }
    }
}
