using DonationApp.Core.Interfaces;
using DonationApp.Core.Shared;
using DonationApp.UseCase.Models;


namespace DonationApp.UseCase.UseCases
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(IModel model);
        Task<Result<TokenModel>> LoginAsync(IModel model);
    }
}
