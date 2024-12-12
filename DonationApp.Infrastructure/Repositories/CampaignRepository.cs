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
                                            .Include(c => c.CampaignLikeCount)
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

        public async Task<IEnumerable<Campaign>> GetAllCampaignByUserId(string userId)
        {
            return await _dbSet.Where(c => c.OrganizationId == userId)
                                .Include(c => c.CampaignAccount)
                                .Include(c => c.Organization)
                                .ToListAsync();
        }

        public async Task<bool> DeactivateCampaign(int id)
        {
            var c = await _dbSet.FindAsync(id);

            if (c == null)
            {
                return false;
            }
            else
            {
                if (c.IsActivated == false)
                {
                    return false;
                }
                c.IsActivated = false;
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> ActivateCampaign(int id)
        {
            var c = await _dbSet.FindAsync(id);

            if (c == null)
            {
                return false;
            }
            else
            {
                if (c.IsActivated == true)
                {
                    return false;
                }
                c.IsActivated = true;
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
