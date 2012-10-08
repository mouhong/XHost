using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands
{
    public class CommandOption
    {
        public string Name { get; set; }

        public IList<string> Values { get; private set; }

        public CommandOption(string name)
        {
            Name = name;
            Values = new List<string>();
        }
    }
}
