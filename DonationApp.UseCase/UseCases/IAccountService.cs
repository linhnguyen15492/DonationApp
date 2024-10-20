using DonationApp.Core.Interfaces;


namespace DonationApp.UseCase.UseCases
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(IModel model);
        Task<bool> LoginAsync(IModel model);
    }
}
