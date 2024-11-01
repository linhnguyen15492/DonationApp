using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces.Repositories;
using DonationApp.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DonationApp.Infrastructure.Repositories
{
    public class CampaignLikeCountRepository : GenericRepository<CampaignLikeCount>, ICampaignLikeCountRepository
    {
        public CampaignLikeCountRepository(ApplicationContext context) : base(context)
        {
        }

        public Task<CampaignLikeCount?> GetByCampaignIdAsync(int campaignId)
        {
            return _dbSet.Where(c => c.CampaignId == campaignId).FirstOrDefaultAsync();
        }
    }
}
