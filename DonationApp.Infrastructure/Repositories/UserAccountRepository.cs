using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces.Repositories;
using DonationApp.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.Repositories
{
    public class UserAccountRepository : GenericRepository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<UserAccount?> FindByAccountNumberAsync(string accountNumber)
        {
            return await _dbSet.Where(a => a.AccountNumber == accountNumber).FirstOrDefaultAsync();
        }

        public async Task<UserAccount?> FindByUserIdAsync(string userId)
        {
            return await _dbSet.Where(a => a.UserId == userId).FirstOrDefaultAsync();
        }

    }
}
