using DonationApp.Core.Interfaces;
using DonationApp.Core.Shared;

namespace DonationApp.UseCase.UseCases
{
    public interface IDonationManager
    {
        Task<Result<IDto>> MakeDonation(IDto dto);
    }
}
