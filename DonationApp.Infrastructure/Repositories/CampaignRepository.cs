using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces.Repositories;
using DonationApp.Infrastructure.DataContext;
using DonationApp.UseCase.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.Repositories
{
    public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(ApplicationContext context) : base(context)
        {
        }

        public Task<Campaign> GetCampaignByCodeWithAccountAsync(string campaignCode)
        {
            throw new NotImplementedException();
        }
    }
}
