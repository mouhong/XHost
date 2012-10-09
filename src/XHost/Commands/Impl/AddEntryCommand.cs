using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands.Impl
{
    public class AddEntryCommand : ICommand
    {
        public string Name
        {
            get
            {
                return "add";
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
                return "Add an entry.";
            }
        }

        public string Usage
        {
            get
            {
                return "xhost -" + Name + " [ip] [host-name] [-override]";
            }
        }

        public void Execute(CommandLine commandLine, CommandExecutionContext context)
        {
            if (commandLine.Parameters.Count != 2)
            {
                context.Output.ErrorLine("Please enter the IP and the host name as parameters. For example: -" + commandLine.CommandName + " 127.0.0.1 mouhong.me");
                return;
            }

            var host = commandLine.Parameters[1];
            var ip = commandLine.Parameters[0];

            if (context.HostFile.Contains(host))
            {
                if (commandLine.Options.Count > 0 && commandLine.Options[0].Name == "override")
                {
                    context.HostFile.Set(ip, host);
                    context.HostFile.Save();

                    context.Output.WriteLine("And entry updated: " + commandLine.Parameters[0] + " " + commandLine.Parameters[1]);
                }
                else
                {
                    context.Output.ErrorLine(host + " already exists. Use -override option to override the existing entry.");
                }
            }
            else
            {
                context.HostFile.Set(commandLine.Parameters[0], commandLine.Parameters[1]);
                context.HostFile.Save();

                context.Output.WriteLine("New entry added: " + commandLine.Parameters[0] + " " + commandLine.Parameters[1]);
            }
        }
    }
}
