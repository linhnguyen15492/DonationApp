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
    }
}
