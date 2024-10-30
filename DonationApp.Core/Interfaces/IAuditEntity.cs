using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Interfaces
{
    public interface IAuditEntity
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
        DateTime UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }

    public interface IAuditEntity<TKey> : IAuditEntity, IEntityBase<TKey>
    {
    }
}
