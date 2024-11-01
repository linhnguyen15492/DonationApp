using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();

        Task BeginTransactionAsync();
        Task<int> SaveChangesAsync();
        Task CancelAsync();
    }
}
