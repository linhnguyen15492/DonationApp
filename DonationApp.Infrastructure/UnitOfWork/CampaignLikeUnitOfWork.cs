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

        public ICampaignLikeRepository CampaignLikeRepository { get; }

        public ICampaignLikeCountRepository CampaignLikeCounterRepository { get; }

        public CampaignLikeUnitOfWork(ApplicationContext context, ICampaignLikeRepository campaignLikeRepository, ICampaignLikeCountRepository campaignLikeCounterRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            CampaignLikeRepository = campaignLikeRepository ?? throw new ArgumentNullException(nameof(campaignLikeRepository));
            CampaignLikeCounterRepository = campaignLikeCounterRepository ?? throw new ArgumentNullException(nameof(campaignLikeCounterRepository));
        }


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
