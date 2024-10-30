using DonationApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Entities
{
    public abstract class AuditEntity<TKey> : DeleteEntity<TKey>, IAuditEntity<TKey>
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
    }
}
