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

        public override async Task<Campaign?> GetByIdAsync(object id)
        {
            Campaign? data = await _dbSet.Where(c => c.Id == (int)id)
                                            .Include(c => c.CampaignAccount)
                                            .Include(c => c.Organization)
                                            .Include(c => c.Comments)
                                                .ThenInclude(c => c.ApplicationUser)
                                            .FirstOrDefaultAsync();

            if (data == null)
            {
                throw new Exception("Not Found");
            }
            else
            {
                return data;
            }
        }

        public Task<Campaign> GetCampaignByCodeWithAccountAsync(string campaignCode)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<Campaign>> GetAllAsync()
        {
            return await _dbSet.Include(c => c.CampaignAccount)
                                .Include(c => c.Organization)
                                .ToListAsync();
        }
    }
}
