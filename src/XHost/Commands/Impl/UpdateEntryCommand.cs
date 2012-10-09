using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands.Impl
{
    public class UpdateEntryCommand : ICommand
    {
        public string Name
        {
            get
            {
                return "update";
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
                return "Update an entry.";
            }
        }

        public string Usage
        {
            get
            {
                return "xhost -" + Name + " [ip] [host]";
            }
        }

        public void Execute(CommandLine commandLine, CommandExecutionContext context)
        {
            if (commandLine.Parameters.Count != 2)
            {
                context.Output.ErrorLine("Invalid request. Please check syntax: " + Usage);
                return;
            }

            var hostFile = context.HostFile;

            var host = commandLine.Parameters[1];
            var ip = commandLine.Parameters[0];

            if (!hostFile.Contains(host))
            {
                context.Output.ErrorLine(host + " was not found");
            }
            else
            {
                hostFile.Set(ip, host);
                hostFile.Save();
                context.Output.WriteLine("One entry updated: " + ip + " " + host);
            }
        }
    }
}
