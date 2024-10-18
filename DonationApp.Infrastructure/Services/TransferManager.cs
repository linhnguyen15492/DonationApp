using DonationApp.Core.Interfaces;
using DonationApp.Core.Shared;
using DonationApp.UseCase.Models;
using DonationApp.UseCase.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.Services
{
    public class TransferManager : ITransferManager
    {
        public Task<Result<IDto>> TransferAsync(IDto dto)
        {
            var model = (TransferModel)dto;
            throw new NotImplementedException();
        }
    }
}
