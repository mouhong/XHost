using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XHost.Commands;
using Xunit;

namespace XHost.Tests.Commands
{
    public class CommandLineFacts
    {
        public class TheParseMethod
        {
            [Fact]
            public void command_with_params_and_valueless_options()
            {
                var commandLine = CommandLine.Parse("-add 127.0.0.1 test.com -override");
                Assert.Equal("add", commandLine.CommandName);
                Assert.Equal(new[] { "127.0.0.1", "test.com" }, commandLine.Parameters.ToArray());
                Assert.Equal(1, commandLine.Options.Count);
                Assert.Equal("override", commandLine.Options[0].Name);
            }
        }
    }
}
