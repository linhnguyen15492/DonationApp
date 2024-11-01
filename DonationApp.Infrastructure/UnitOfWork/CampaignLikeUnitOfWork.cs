using DonationApp.Core.Interfaces.Repositories;
using DonationApp.Infrastructure.DataContext;
using DonationApp.UseCase.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.UnitOfWork
{
    public class CampaignLikeUnitOfWork : ICampaignLikeUnitOfWork
    {
        private readonly ApplicationContext _context;

        public CampaignLikeUnitOfWork(ApplicationContext context)
        {
            _context=context;
        }
        public ICampaignLikeRepository CampaignLikeRepository => throw new NotImplementedException();

        public ICampaignLikeCountRepository CampaignLikeCounterRepository => throw new NotImplementedException();

        public Task BeginTransactionAsync()
        {
            return Task.CompletedTask;
        }

        public Task CancelAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
