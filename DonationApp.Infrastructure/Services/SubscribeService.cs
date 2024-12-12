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

        public SubscribeService(ISubcribeCampaignRepository subcribeCampaignRepository)
        {
            _subcribeCampaignRepository = subcribeCampaignRepository;
        }

        public async Task<bool> ApproveSubscriber(int campaignId, string userId)
        {
            return await _subcribeCampaignRepository.ApproveSubscriber(campaignId, userId);
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
                AccountNumber = x.ApplicationUser.UserAccount!.AccountNumber,
                IsVerified = x.IsVerified
            });

            return s;

        }

        public async Task<bool> RejectSubscriber(int campaignId, string userId)
        {
            return await _subcribeCampaignRepository.RejectSubscriber(campaignId, userId);
        }
    }
}
