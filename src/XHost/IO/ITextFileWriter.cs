using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.IO
{
    public interface ITextFileWriter
    {
        void Write(string path, string content, Encoding encoding);
    }
}
