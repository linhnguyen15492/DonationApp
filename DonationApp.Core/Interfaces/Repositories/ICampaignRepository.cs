using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.Repositories
{
    public interface ICampaignRepository : IGenericRepository<Campaign>
    {
        Task<Campaign> GetCampaignByCodeWithAccountAsync(string campaignCode);

        Task<IEnumerable<Campaign>> GetAllCampaignByUserId(string userId);
    }
}
