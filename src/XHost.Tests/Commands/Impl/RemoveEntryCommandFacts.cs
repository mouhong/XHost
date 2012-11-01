using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using XHost.Commands;
using XHost.Commands.Impl;
using XHost.Tests.Mocks;
using Xunit;

namespace XHost.Tests.Commands.Impl
{
    public class RemoveEntryCommandFacts
    {
        [Fact]
        public void can_remove_mulit_entries_separated_by_comma()
        {
            var commandLine = CommandLine.Parse("-remove test1.com,test2.com");

            var context = new CommandExecutionContext(new HostsFile(new MockTextFileWriter()));
            context.Hosts["test1.com"] = "192.168.1.2";
            context.Hosts["test2.com"] = "192.168.1.3";
            context.Hosts["test3.com"] = "129.168.1.4";

            var command = new RemoveEntryCommand();
            command.Execute(commandLine, context);

            Assert.Equal(1, context.Hosts.AllEntries().Count);
            AssertEntry(context.Hosts.AllEntries()[0], "129.168.1.4", "test3.com");
        }

        [Fact]
        public void can_remove_multi_entries_separated_by_comma_space()
        {
            var commandLine = CommandLine.Parse("-remove test1.com, test2.com");

            var context = new CommandExecutionContext(new HostsFile(new MockTextFileWriter()));
            context.Hosts["test1.com"] = "192.168.1.2";
            context.Hosts["test2.com"] = "192.168.1.3";
            context.Hosts["test3.com"] = "129.168.1.4";

            var command = new RemoveEntryCommand();
            command.Execute(commandLine, context);

            Assert.Equal(1, context.Hosts.AllEntries().Count);
            AssertEntry(context.Hosts.AllEntries()[0], "129.168.1.4", "test3.com");
        }

        private void AssertEntry(HostsFileLine entry, string expectedIP, string expectedHost)
        {
            Assert.Equal(expectedIP, entry.IP);
            Assert.Equal(expectedHost, entry.Host);
        }
    }
}
