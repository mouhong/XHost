﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands.Impl
{
    public class FindEntryCommand : ICommand
    {
        public string Name
        {
            get
            {
                return "find";
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
                return "Find an entry by host name.";
            }
        }

        public string Usage
        {
            get
            {
                return "xhost -" + Name + " [host-name]";
            }
        }

        public void Execute(CommandLine commandLine, CommandExecutionContext context)
        {
            if (commandLine.Parameters.Count == 0)
            {
                context.Output.ErrorLine(commandLine.CommandName + " command requires arguments.");
                return;
            }

            var entry = context.HostFile.FindEntry(commandLine.Parameters[0]);

            if (entry == null)
            {
                context.Output.WriteLine("No entry was found.");
            }
            else
            {
                context.Output.WriteLine(entry.IP + " " + entry.Host);
            }
        }
    }
}
