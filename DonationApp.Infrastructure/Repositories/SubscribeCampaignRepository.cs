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
    public class SubscribeCampaignRepository : GenericRepository<SubscribeCampaign>, ISubcribeCampaignRepository
    {
        public SubscribeCampaignRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<SubscribeCampaign?> GetByCampaignIdAndUserId(int campaignId, string userId)
        {
            return await _dbSet.Where(s => s.CampaignId == campaignId && s.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
