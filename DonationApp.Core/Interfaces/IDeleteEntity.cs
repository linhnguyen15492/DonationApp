using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Interfaces
{
    public interface IDeleteEntity
    {
        bool IsDeleted { get; set; }
    }

    public interface IDeleteEntity<TKey> : IDeleteEntity, IEntityBase<TKey>
    {
    }
}
