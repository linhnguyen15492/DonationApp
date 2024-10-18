using DonationApp.Core.Interfaces;
using DonationApp.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.UseCases
{
    public interface ITransferManager
    {
        Task<Result<IDto>> TransferAsync(IDto dto);
    }
}
