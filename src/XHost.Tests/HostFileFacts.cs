using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace XHost.Tests
{
    public class HostFileFacts
    {
        public class TheLoadMethod
        {
            [Fact]
            public void can_load_both_comments_and_entries()
            {
                var hosts = "# comment line 1" + Environment.NewLine
                    + "127.0.0.1    entry1.com" + Environment.NewLine
                    + "127.0.0.1 entry2.com" + Environment.NewLine
                    + "216.92.92.92 entry3.com" + Environment.NewLine
                    + "# comment line 2 " + Environment.NewLine
                    + "127.0.0.1 entry4.com";

                var reader = new StringReader(hosts);
                var hostFile = HostFile.Load(reader);

                Assert.Equal(6, hostFile.Lines.Count);
                Assert.Equal(4, hostFile.AllEntries().Count);
            }
        }

        public class TheSaveAsMethod
        {
            [Fact]
            public void will_keep_original_comments_and_empty_lines_not_changed()
            {
                var hosts = "# comment line 1" + Environment.NewLine
                    + "127.0.0.1 entry1.com" + Environment.NewLine
                    + Environment.NewLine
                    + "# comment line 2 " + Environment.NewLine
                    + "127.0.0.1 entry4.com";

                var reader = new StringReader(hosts);
                var hostFile = HostFile.Load(reader);

                hostFile["entry5.com"] = "127.0.0.1";

                var writer = new StringWriter();
                hostFile.SaveAs(writer);

                Assert.Equal(hosts + Environment.NewLine + "127.0.0.1 entry5.com" + Environment.NewLine, writer.ToString());
            }
        }

        public class TheSetMethod
        {
            [Fact]
            public void will_update_IsDirty_property_if_and_only_if_real_changes_are_made()
            {
                var hostFile = new HostFile();
                hostFile.Set("127.0.0.1", "test.com");
                Assert.True(hostFile.IsDirty);

                hostFile.MarkClean();

                hostFile.Set("127.0.0.1", "test1.com");
                Assert.True(hostFile.IsDirty);

                hostFile.Set("127.0.0.1", "test1.com");
                Assert.False(hostFile.IsDirty);
            }
        }

        public class TheRemoveMethod
        {
            [Fact]
            public void can_remove_entry_and_return_success_or_not()
            {
                var hostFile = new HostFile();
                hostFile.Set("127.0.0.1", "test.com");

                Assert.False(hostFile.Remove("test1.com"));
                Assert.False(hostFile.Remove("127.0.0.1"));

                Assert.True(hostFile.Remove("test.com"));
                Assert.Equal(0, hostFile.Lines.Count);
            }

            [Fact]
            public void will_update_IsDirty_property_if_and_only_if_real_changes_are_made()
            {
                var hostFile = new HostFile();
                hostFile.Set("127.0.0.1", "test.com");
                hostFile.MarkClean();

                hostFile.Remove("test1.com");
                Assert.False(hostFile.IsDirty);

                hostFile.Remove("test.com");
                Assert.True(hostFile.IsDirty);
            }
        }

        public class TheSaveMethod
        {
            [Fact]
            public void will_set_IsDirty_to_false()
            {
                var hostFilePath = Path.Combine(Path.GetTempPath(), "hosts");

                var hostFile = new HostFile();
                hostFile.Set("127.0.0.1", "test.com");

                Assert.True(hostFile.IsDirty);

                XHostConfig.HostsFilePath = hostFilePath;
                hostFile.Save();

                Assert.False(hostFile.IsDirty);
            }
        }
    }
}
