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
    public class CampaignAccountRepository : GenericRepository<CampaignAccount>, ICampaignAccountRepository
    {
        public CampaignAccountRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<CampaignAccount?> FindByAccountNumberAsync(string accountNumber)
        {
            return await _dbSet.Where(a => a.AccountNumber == accountNumber).FirstOrDefaultAsync();
        }

        public Task<CampaignAccount?> FindByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
