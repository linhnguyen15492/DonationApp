using DonationApp.Core.Interfaces;
using DonationApp.UseCase.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.Services
{
    public class CampaignLikeService : ICampaignLikeService
    {
        private readonly ICampaignLikeUnitOfWork _campaignLikeUnitOfWork;

        public CampaignLikeService(ICampaignLikeUnitOfWork campaignLikeUnitOfWork)
        {
            _campaignLikeUnitOfWork=campaignLikeUnitOfWork ?? throw new ArgumentNullException(nameof(campaignLikeUnitOfWork));
        }

        public Task<int> DislikeCampaignAsync(IModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> LikeCampaignAsync(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
