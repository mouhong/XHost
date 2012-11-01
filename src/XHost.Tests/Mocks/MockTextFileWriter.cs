using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XHost.IO;

namespace XHost.Tests.Mocks
{
    public class MockTextFileWriter : ITextFileWriter
    {
        public void Write(string path, string content, Encoding encoding)
        {
        }
    }
}
