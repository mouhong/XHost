using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace XHost
{
    public static class XHostConfig
    {
        static readonly string _defaultHostsFilePath = @"C:\Windows\System32\Drivers\etc\hosts";

        public static string HostsFilePath { get; set; }

        static XHostConfig()
        {
            var path = ConfigurationManager.AppSettings["XHosts/HostFilePath"];

            if (String.IsNullOrEmpty(path))
            {
                path = _defaultHostsFilePath;
            }

            HostsFilePath = path;
        }
    }
}
