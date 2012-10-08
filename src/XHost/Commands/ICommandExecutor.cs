using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands
{
    public interface ICommandExecutor
    {
        string Name { get; }

        string ShortName { get; }

        void Execute(Command command, CommandExecutionContext context);
    }
}
