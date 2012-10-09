﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands.Impl
{
    public class RemoveEntryCommand : ICommand
    {
        public string Name
        {
            get
            {
                return "remove";
            }
        }

        public string ShortName
        {
            get
            {
                return "rm";
            }
        }

        public string Description
        {
            get
            {
                return "Remove entries(comma separated).";
            }
        }

        public string Usage
        {
            get
            {
                return "xhost -" + Name + " [hosts]";
            }
        }

        public void Execute(CommandLine commandLine, CommandExecutionContext context)
        {
            if (commandLine.Parameters.Count == 0)
            {
                context.Output.ErrorLine("Invalid request. Please check syntax: " + Usage);
                return;
            }

            var removedHosts = 0;

            var hosts = from param in commandLine.Parameters
                        from host in param.Split(',').Select(x => x.Trim())
                        where !String.IsNullOrEmpty(host)
                        select host;

            foreach (var host in hosts)
            {
                if (context.HostFile.Remove(host)) removedHosts++;
            }

            context.HostFile.Save();

            context.Output.WriteLine(removedHosts + " entries removed");
        }
    }
}
