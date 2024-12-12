using DonationApp.Core.Entities;
using DonationApp.Core.Enums;
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

        public async Task<bool> ApproveSubscriber(int campaignId, string userId)
        {
            var record = await _dbSet.Where(s => s.CampaignId == campaignId && s.UserId == userId).FirstOrDefaultAsync();

            if (record == null)
            {
                return false;
            }

            record.SubscribeStatus = SubscribeStatusEnum.Verified.ToString();
            record.IsVerified = true;
            await _context.SaveChangesAsync();
            return true;
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

        public async Task<bool> RejectSubscriber(int campaignId, string userId)
        {
            var record = await _dbSet.Where(s => s.CampaignId == campaignId && s.UserId == userId).FirstOrDefaultAsync();

            if (record == null)
            {
                return false;
            }

            record.SubscribeStatus = SubscribeStatusEnum.Rejected.ToString();
            record.IsVerified = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
