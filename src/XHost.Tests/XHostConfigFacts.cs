using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace XHost.Tests
{
    public class XHostConfigFacts
    {
        public class TheHostsFilePathProperty
        {
            [Fact]
            public void should_return_default_path_if_not_explicit_configured()
            {
                var content = "key1 = val1";
                var config = XHostConfig.LoadFrom(new StringReader(content));
                Assert.Equal(XHostConfig.DefaultHostsFilePath, config.HostsFilePath);
            }

            [Fact]
            public void should_return_configured_value_if_explicit_configured()
            {
                var content = "HostsFilePath = D:\\hosts";
                var config = XHostConfig.LoadFrom(new StringReader(content));
                Assert.Equal("D:\\hosts", config.HostsFilePath);
            }
        }

        public class TheLoadFromMethod
        {
            [Fact]
            public void can_load_configs()
            {
                var content = "key1 = val1";
                var config = XHostConfig.LoadFrom(new StringReader(content));
                Assert.Equal("val1", config.Get("key1"));

                content = "key1 = val1" + Environment.NewLine
                        + "key2= val2" + Environment.NewLine
                        + Environment.NewLine
                        + " HostsFilePath =D:\\hosts" + Environment.NewLine
                        + Environment.NewLine;

                config = XHostConfig.LoadFrom(new StringReader(content));

                Assert.Equal(3, config.Items.Count);
                Assert.Equal("val1", config.Get("key1"));
                Assert.Equal("val2", config.Get("key2"));
                Assert.Equal("D:\\hosts", config.Get("HostsFilePath"));
                Assert.Equal("D:\\hosts", config.HostsFilePath);
            }
        }
    }
}
