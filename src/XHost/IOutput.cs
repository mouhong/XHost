using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost
{
    public interface IOutput
    {
        void WriteLine();

        void WriteLine(string message);

        void WarnLine(string message);

        void ErrorLine(string message);
    }
}
