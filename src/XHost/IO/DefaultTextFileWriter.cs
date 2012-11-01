using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XHost.IO
{
    public class DefaultTextFileWriter : ITextFileWriter
    {
        public void Write(string path, string content, Encoding encoding)
        {
            using (var writer = new StreamWriter(path, false, encoding))
            {
                writer.Write(content);
                writer.Flush();
            }
        }
    }
}
