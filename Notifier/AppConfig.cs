using System;
using System.Collections.Generic;
using System.Text;

namespace Notifier
{
    public class AppConfig
    {
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool SSLActive { get; set; }
    }
}
