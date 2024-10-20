using DonationApp.Core.Interfaces;
using DonationApp.UseCase.UseCases;

namespace DonationApp.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        public Task<bool> LoginAsync(IModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
