using DonationApp.Core.Interfaces;
using DonationApp.Core.Shared;
using DonationApp.UseCase.Models;
using DonationApp.UseCase.UseCases;

namespace DonationApp.Infrastructure.Services
{
    public class TransferManager : ITransferManager
    {
        public Task<Result<IDto>> TransferAsync(IModel model)
        {
            var data = model as TransferModel;
            throw new NotImplementedException();
        }
    }
}
