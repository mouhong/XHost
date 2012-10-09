using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XHost
{
    public class HostsFile
    {
        public IList<HostsFileLine> Lines { get; private set; }

        public bool IsDirty { get; private set; }

        public string this[string hostName]
        {
            get
            {
                return GetIPAddress(hostName);
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("IP address is required.", "value");

                Set(value, hostName);
            }
        }

        public HostsFile()
        {
            Lines = new List<HostsFileLine>();
        }

        public static HostsFile Load()
        {
            return Load(XHostConfig.Instance.HostsFilePath);
        }

        public static HostsFile Load(string path)
        {
            var file = new HostsFile();
            file.ReloadFrom(path);
            return file;
        }

        public static HostsFile Load(TextReader reader)
        {
            var file = new HostsFile();
            file.ReloadFrom(reader);
            return file;
        }

        public void ReloadFrom(string path)
        {
            using (var reader = new StreamReader(path))
            {
                ReloadFrom(reader);
            }
        }

        public void ReloadFrom(TextReader reader)
        {
            Lines.Clear();

            while (true)
            {
                var line = reader.ReadLine();

                if (line == null) break;

                Lines.Add(new HostsFileLine(line));
            }
        }

        public HostsFileLine FindEntry(string host)
        {
            if (String.IsNullOrEmpty(host)) return null;

            return Lines.FirstOrDefault(x => x.Host == host);
        }

        public string GetIPAddress(string host)
        {
            var entry = FindEntry(host);
            return entry == null ? null : entry.IP;
        }

        public IList<HostsFileLine> AllEntries()
        {
            if (Lines.Count == 0)
            {
                return new List<HostsFileLine>();
            }

            return Lines.Where(x => x.IsEntry).ToList();
        }

        public bool Contains(string host)
        {
            return Lines.Any(x => x.Host == host);
        }

        public void Set(string ip, string host)
        {
            var line = FindEntry(host);

            if (line != null)
            {
                if (line.IP != ip)
                {
                    line.Update(ip, host);
                    IsDirty = true;
                }
            }
            else
            {
                line = new HostsFileLine(ip, host);
                Lines.Add(line);
                IsDirty = true;
            }
        }

        public bool Remove(string host)
        {
            var line = FindEntry(host);

            if (line != null)
            {
                Lines.Remove(line);
                IsDirty = true;
                return true;
            }

            return false;
        }

        public void MarkClean()
        {
            IsDirty = false;
        }

        public void Save()
        {
            if (!IsDirty) return;

            SaveAs(XHostConfig.Instance.HostsFilePath);
            IsDirty = false;
        }

        public void SaveAs(string path)
        {
            using (var writer = new StreamWriter(path, false, Encoding.Default))
            {
                SaveAs(writer);
            }
        }

        public void SaveAs(TextWriter writer)
        {
            foreach (var line in Lines)
            {
                writer.WriteLine(line.Text);
            }
        }
    }
}
