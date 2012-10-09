using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands.Impl
{
    public class PathCommand : ICommand
    {
        public string Name
        {
            get
            {
                return "path";
            }
        }

        public string ShortName
        {
            get
            {
                return null;
            }
        }

        public string Description
        {
            get
            {
                return "Display the hosts file path.";
            }
        }

        public string Usage
        {
            get
            {
                return "xhost -" + Name;
            }
        }

        public void Execute(CommandLine commandLine, CommandExecutionContext context)
        {
            Console.WriteLine(XHostConfig.Instance.HostsFilePath);
        }
    }
}
