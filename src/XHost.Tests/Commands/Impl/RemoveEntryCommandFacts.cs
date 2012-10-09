using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using XHost.Commands;
using Xunit;

namespace XHost.Tests.Commands.Impl
{
    public class RemoveEntryCommandFacts
    {
        [Fact]
        public void can_remove_mulit_entries_separated_by_comma()
        {
            CommandFactory.Clear();
            CommandFactory.Register(Assembly.Load("XHost"));

            var commandLine = CommandLine.Parse("-remove test1.com,test2.com");
            var command = CommandFactory.Find(commandLine.CommandName);

            var context = new CommandExecutionContext(new HostFile());
            context.HostFile["test1.com"] = "192.168.1.2";
            context.HostFile["test2.com"] = "192.168.1.3";
            context.HostFile["test3.com"] = "129.168.1.4";

            command.Execute(commandLine, context);

            Assert.Equal(1, context.HostFile.AllEntries().Count);
            AssertEntry(context.HostFile.AllEntries()[0], "129.168.1.4", "test3.com");
        }

        [Fact]
        public void can_remove_multi_entries_separated_by_comma_space()
        {
            CommandFactory.Clear();
            CommandFactory.Register(Assembly.Load("XHost"));

            var commandLine = CommandLine.Parse("-remove test1.com, test2.com");
            var command = CommandFactory.Find(commandLine.CommandName);

            var context = new CommandExecutionContext(new HostFile());
            context.HostFile["test1.com"] = "192.168.1.2";
            context.HostFile["test2.com"] = "192.168.1.3";
            context.HostFile["test3.com"] = "129.168.1.4";

            command.Execute(commandLine, context);

            Assert.Equal(1, context.HostFile.AllEntries().Count);
            AssertEntry(context.HostFile.AllEntries()[0], "129.168.1.4", "test3.com");
        }

        private void AssertEntry(HostFileLine entry, string expectedIP, string expectedHost)
        {
            Assert.Equal(expectedIP, entry.IP);
            Assert.Equal(expectedHost, entry.Host);
        }
    }
}
