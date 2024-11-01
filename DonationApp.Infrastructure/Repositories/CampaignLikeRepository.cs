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
    public class CampaignLikeRepository : GenericRepository<CampaignLike>, ICampaignLikeRepository
    {
        public CampaignLikeRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<CampaignLike?> GetByUserIdAndCampaignId(string userId, int campaignId)
        {
            return await _dbSet.Where(c => c.CampaignId == campaignId && c.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
