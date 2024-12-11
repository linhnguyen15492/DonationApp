using DonationApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Interfaces.Repositories
{
    public interface ISubcribeCampaignRepository : IGenericRepository<SubscribeCampaign>
    {
        public Task<SubscribeCampaign?> GetByCampaignIdAndUserId(int campaignId, string userId);
    }
}
