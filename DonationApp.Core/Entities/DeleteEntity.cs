using DonationApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Entities
{
    public abstract class DeleteEntity<TKey> : EntityBase<TKey>, IDeleteEntity<TKey>
    {
        public bool IsDeleted { get; set; }
    }
}
