using DonationApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Interfaces.Repositories
{
    public interface IBankAccountRepository<T> where T : class
    {
        Task<T?> FindByIdAsync(string accountNumber);
        Task<IEnumerable<T>> FindByUserIdAsync(Guid userId);
    }
}
