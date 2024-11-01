using DonationApp.Core.Interfaces;
using DonationApp.Core.Shared;
using DonationApp.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.UseCases
{
    public interface ITransferManager
    {
        Task<TransactionResult> DonateAsync(IModel model);
        Task<TransactionResult> DisburseAsync(IModel model);
    }
}
