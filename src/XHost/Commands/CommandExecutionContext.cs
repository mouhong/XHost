using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands
{
    public class CommandExecutionContext
    {
        public HostsFile Hosts { get; private set; }

        public CommandExecutionContext(HostsFile hostFile)
        {
            Hosts = hostFile;
        }
    }
}
