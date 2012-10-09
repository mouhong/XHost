using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace XHost
{
    public class XHostConfig
    {
        public static readonly string DefaultHostsFilePath = @"C:\Windows\System32\Drivers\etc\hosts";

        private static XHostConfig _instance;

        public static XHostConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Load();
                }

                return _instance;
            }
        }

        private NameValueCollection _items = new NameValueCollection();

        public NameValueCollection Items
        {
            get
            {
                return _items;
            }
        }

        public string HostsFilePath
        {
            get
            {
                var path = Get("HostsFilePath");
                return String.IsNullOrEmpty(path) ? DefaultHostsFilePath : path;
            }
            set
            {
                Set("HostsFilePath", value);
            }
        }

        public string Get(string key)
        {
            return _items[key];
        }

        public void Set(string key, string value)
        {
            _items[key] = value;
        }

        public static XHostConfig Load()
        {
            return LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt"));
        }

        public static XHostConfig LoadFrom(string path)
        {
            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path, Encoding.UTF8))
                {
                    return LoadFrom(reader);
                }
            }

            return new XHostConfig();
        }

        public static XHostConfig LoadFrom(TextReader reader)
        {
            var config = new XHostConfig();

            while (true)
            {
                var line = reader.ReadLine();

                if (line == null) break;

                if (!String.IsNullOrEmpty(line))
                {
                    var parts = line.Split('=').Select(x => x.Trim()).ToList();

                    if (parts.Count > 0)
                    {
                        var key = parts[0];
                        var value = String.Empty;

                        if (parts.Count > 1)
                        {
                            value = parts[1];
                        }

                        config.Set(key, value);
                    }
                }
            }

            return config;
        }
    }
}
