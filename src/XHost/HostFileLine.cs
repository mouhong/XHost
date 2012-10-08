using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost
{
    public class HostFileLine
    {
        public string Text { get; private set; }

        public string Host { get; private set; }

        public string IP { get; private set; }

        public bool IsComment
        {
            get
            {
                return Text.Length > 0 && Text[0] == '#';
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Text.Length == 0;
            }
        }

        public bool IsEntry
        {
            get
            {
                return !IsEmpty && !IsComment && !String.IsNullOrEmpty(Host) && !String.IsNullOrEmpty(IP);
            }
        }

        public HostFileLine()
        {
        }

        public HostFileLine(string text)
        {
            Update(text);
        }

        public HostFileLine(string ip, string host)
        {
            Update(ip, host);
        }

        public void Update(string ip, string host)
        {
            if (String.IsNullOrEmpty(ip))
                throw new ArgumentException("'ip' is required.");
            if (String.IsNullOrEmpty(host))
                throw new ArgumentException("'host' is required.");

            IP = ip;
            Host = host;
            Text = IP + " " + Host;
        }

        public void Update(string text)
        {
            Text = (text ?? String.Empty).Trim();
            ParseText();
        }

        private void ParseText()
        {
            if (!IsEmpty && !IsComment)
            {
                var parts = Text.Split(' ').Select(x => x.Trim()).Where(x => !String.IsNullOrEmpty(x)).ToList();

                if (parts.Count == 2)
                {
                    IP = parts[0];
                    Host = parts[1];
                }
            }
        }
    }
}
