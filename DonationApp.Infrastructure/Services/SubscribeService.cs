using DonationApp.Core.Interfaces.Repositories;
using DonationApp.UseCase.Dtos;
using DonationApp.UseCase.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly ISubcribeCampaignRepository _subcribeCampaignRepository;

        private readonly IUserAccountRepository _userAccountRepository;

        public SubscribeService(ISubcribeCampaignRepository subcribeCampaignRepository, IUserAccountRepository userAccountRepository)
        {
            _subcribeCampaignRepository = subcribeCampaignRepository;
            _userAccountRepository = userAccountRepository;
        }

        public async Task<IEnumerable<Subscribers>> GetSubscribersByCampaignId(int campaignId)
        {
            var res = await _subcribeCampaignRepository.GetSubscribersByCampaignId(campaignId);

            var s = res.Select(x => new Subscribers
            {
                UserId = x.UserId,
                FullName = x.ApplicationUser!.FullName,
                Address = x.ApplicationUser.Address,
                Email = x.ApplicationUser.Email,
                SubscribeStatus = x.SubscribeStatus,
                PhoneNumber = x.ApplicationUser.PhoneNumber,
                AccountNumber = x.ApplicationUser.UserAccount!.AccountNumber
            });

            return s;

        }
    }
}
