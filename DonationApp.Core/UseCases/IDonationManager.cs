using DonationApp.Core.Interfaces;
using DonationApp.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.UseCases
{
    public interface IDonationManager
    {
        Task<Result<IDto>> MakeDonation(IDto dto);
    }
}
