using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands
{
    public interface ICommand
    {
        string Name { get; }

        string ShortName { get; }

        string Description { get; }

        string Usage { get; }

        void Execute(CommandLine commandLine, CommandExecutionContext context);
    }
}
