using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands
{
    public class CommandExecutionContext
    {
        public HostFile HostFile { get; private set; }

        public IOutput Output { get; private set; }

        public CommandExecutionContext(HostFile hostFile, IOutput output)
        {
            HostFile = hostFile;
            Output = output;
        }
    }
}
