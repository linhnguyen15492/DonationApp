using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using DonationApp.UseCase.Models;
using DonationApp.UseCase.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DonationApp.Infrastructure.Services
{
    public class CampaignLikeService : ICampaignLikeService
    {
        private readonly ICampaignLikeUnitOfWork _campaignLikeUnitOfWork;

        public CampaignLikeService(ICampaignLikeUnitOfWork campaignLikeUnitOfWork)
        {
            _campaignLikeUnitOfWork = campaignLikeUnitOfWork ?? throw new ArgumentNullException(nameof(campaignLikeUnitOfWork));
        }

        public Task<int> DislikeCampaignAsync(IModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<int> LikeCampaignAsync(IModel model)
        {
            var input = (LikeCampaignModel)model;

            if (input is not null)
            {
                if (await IsUserLikeCampaign(input.UserId, input.CampaignId))
                {
                    return -1;
                }

                await _campaignLikeUnitOfWork.BeginTransactionAsync();

                await _campaignLikeUnitOfWork.CampaignLikeRepository.InsertAsync(new CampaignLike()
                {
                    UserId = input.UserId,
                    CampaignId = input.CampaignId
                });

                var data = await _campaignLikeUnitOfWork.CampaignLikeCounterRepository.GetByCampaignIdAsync(input.CampaignId);

                if (data is null)
                {
                    await _campaignLikeUnitOfWork.CampaignLikeCounterRepository.InsertAsync(new CampaignLikeCount()
                    {
                        CampaignId = input.CampaignId,
                        Count = 1
                    });
                }
                else
                {
                    data.Count += 1;
                    await _campaignLikeUnitOfWork.CampaignLikeCounterRepository.UpdateAsync(data);
                }

                return await _campaignLikeUnitOfWork.SaveChangesAsync();
            }

            return -1;
        }


        private async Task<bool> IsUserLikeCampaign(string userId, int campaignId)
        {
            var record = await _campaignLikeUnitOfWork.CampaignLikeRepository.GetByUserIdAndCampaignId(userId, campaignId);

            if (record is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}