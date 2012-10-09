using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XHost.Commands;

namespace XHost.Commands.Tests
{
    public class CommandOptionCollectionFacts
    {
        public class TheParseMethod
        {
            [Fact]
            public void will_return_empty_option_collection_for_empty_or_null_args_input()
            {
                Assert.Equal(0, CommandOptionCollection.Parse(null).Count);
                Assert.Equal(0, CommandOptionCollection.Parse(new List<string>()).Count);
            }

            [Fact]
            public void can_parse_single_param()
            {
                // without param value
                var options = CommandOptionCollection.Parse(new[] { "-param1" });
                Assert.Equal(1, options.Count);
                AssertOption(options[0], "param1", null);
                
                // with 1 value
                options = CommandOptionCollection.Parse(new[] { "-param1", "val1" });
                Assert.Equal(1, options.Count);
                AssertOption(options[0], "param1", "val1");

                // with multi values
                options = CommandOptionCollection.Parse(new[] { "-param1", "val1", "val2" });
                Assert.Equal(1, options.Count);
                AssertOption(options[0], "param1", "val1", "val2");
            }

            [Fact]
            public void can_parse_multi_params()
            {
                var options = CommandOptionCollection.Parse(new[] { "-param1", "-param2" });
                Assert.Equal(2, options.Count);
                AssertOption(options[0], "param1", null);
                AssertOption(options[1], "param2", null);

                options = CommandOptionCollection.Parse(new[] { "-param1", "val1", "-param2" });
                Assert.Equal(2, options.Count);
                AssertOption(options[0], "param1", "val1");
                AssertOption(options[1], "param2", null);

                options = CommandOptionCollection.Parse(new[] { "-param1", "val1", "-param2", "-param3", "val3_1", "val3_2" });
                Assert.Equal(3, options.Count);
                AssertOption(options[0], "param1", "val1");
                AssertOption(options[1], "param2", null);
                AssertOption(options[2], "param3", "val3_1", "val3_2");
            }

            private void AssertOption(CommandOption option, string expectedName, params string[] expectedValues)
            {
                Assert.Equal(expectedName, option.Name);

                if (expectedValues == null || expectedValues.Length == 0)
                {
                    Assert.Equal(0, option.Values.Count);
                }
                else
                {
                    Assert.Equal(expectedValues, option.Values.ToArray());
                }
            }
        }
    }
}
