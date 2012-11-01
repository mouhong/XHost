using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XHost.Tests
{
    public class HostFileLineFacts
    {
        public class TheIsCommentProperty
        {
            [Fact]
            public void can_recognize_comment()
            {
                var line = new HostsFileLine("# hello world");
                Assert.True(line.IsComment);
                Assert.False(line.IsEntry);
                Assert.False(line.IsEmpty);

                line = new HostsFileLine("#hello world");
                Assert.True(line.IsComment);
            }
        }

        public class TheIsEmptyProperty
        {
            [Fact]
            public void can_recognize_empty_line()
            {
                var line = new HostsFileLine(Environment.NewLine);
                Assert.True(line.IsEmpty);
                Assert.False(line.IsEntry);
                Assert.False(line.IsComment);
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void will_also_update_Text_if_params_are_ip_and_host()
            {
                var line = new HostsFileLine();
                line.Update("127.0.0.1", "test.com");
                Assert.Equal("127.0.0.1 test.com", line.Text);
            }

            [Fact]
            public void will_parse_text_if_param_is_text()
            {
                var line = new HostsFileLine();
                line.Update("192.168.1.10 test1.com");
                Assert.Equal("192.168.1.10", line.IP);
                Assert.Equal("test1.com", line.Host);
            }

            [Fact]
            public void can_support_dot_shortcut()
            {
                var line = new HostsFileLine();
                line.Update(".", "test.com");
                Assert.Equal("127.0.0.1 test.com", line.Text);
                Assert.Equal("127.0.0.1", line.IP);
            }
        }
    }
}
