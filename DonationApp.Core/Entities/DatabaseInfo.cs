using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Entities
{
    public class DatabaseInfo
    {
        public bool CanConnect { get; set; }
        public string DatabaseName { get; set; } = string.Empty;
        public string Server { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public List<string>? TableNames { get; set; }
        public bool IsSeeded { get; set; }
    }
}
