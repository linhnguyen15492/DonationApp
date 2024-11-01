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
    public class TransactionUnitOfWork : ITransactionUnitOfWork
    {
        private readonly ApplicationContext context;

        public ITransactionRepository TransactionRepository { get; }

        public ICampaignAccountRepository CampaignAccountRepository { get; }

        public IUserAccountRepository UserAccountRepository { get; }

        public TransactionUnitOfWork(ApplicationContext context, ITransactionRepository transactionRepository,
            ICampaignAccountRepository campaignAccountRepository, IUserAccountRepository userAccountRepository)
        {
            this.context=context ?? throw new ArgumentNullException(nameof(context));
            TransactionRepository=transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            CampaignAccountRepository=campaignAccountRepository ?? throw new ArgumentNullException(nameof(campaignAccountRepository));
            UserAccountRepository=userAccountRepository ?? throw new ArgumentNullException(nameof(userAccountRepository));
        }

        public Task BeginTransactionAsync()
        {
            return Task.CompletedTask;
        }

        public Task CancelAsync()
        {
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
