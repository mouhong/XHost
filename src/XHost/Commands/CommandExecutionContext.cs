using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands
{
    public class CommandExecutionContext
    {
        public HostFile HostFile { get; private set; }

        public CommandExecutionContext(HostFile hostFile)
        {
            HostFile = hostFile;
        }
    }
}
