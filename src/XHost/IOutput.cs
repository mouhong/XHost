using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost
{
    public interface IOutput
    {
        void Write(string message);

        void Write(string format, params object[] args);

        void WriteLine();

        void WriteLine(string message);

        void WriteLine(string format, params object[] args);

        void WarnLine(string message);

        void WarnLine(string format, params object[] args);

        void ErrorLine(string message);

        void ErrorLine(string format, params object[] args);
    }
}
