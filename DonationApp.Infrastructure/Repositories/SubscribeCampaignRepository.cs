using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces.Repositories;
using DonationApp.Infrastructure.DataContext;
using DonationApp.UseCase.Dtos;
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

        public async Task<IEnumerable<SubscribeCampaign>> GetSubscribersByCampaignId(int campaignId)
        {
            return await _dbSet.Include(c => c.ApplicationUser).ThenInclude(c => c!.UserAccount)
                                .Where(c => c.CampaignId == campaignId).ToListAsync();
        }
    }
}
