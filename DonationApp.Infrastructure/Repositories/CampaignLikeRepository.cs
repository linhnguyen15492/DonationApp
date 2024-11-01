using DonationApp.Core.Entities;
using DonationApp.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.Repositories
{
    public class CampaignLikeRepository : GenericRepository<CampaignLike>
    {
        public CampaignLikeRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
