using DonationApp.UseCase.UseCases;

namespace DonationApp.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        public Task<bool> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
