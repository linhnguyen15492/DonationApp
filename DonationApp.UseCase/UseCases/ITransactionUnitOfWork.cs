using DonationApp.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.UseCases
{
    public interface ITransactionUnitOfWork
    {
        ITransactionRepository TransactionRepository { get; }
        ICampaignAccountRepository CampaignAccountRepository { get; }
        IUserAccountRepository UserAccountRepository { get; }

        Task BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CancelAsync(); // this method should be called ASAP before leaving, a ITransactionUnitOfWork implementation should implement IDisposable to handle that
    }
}
