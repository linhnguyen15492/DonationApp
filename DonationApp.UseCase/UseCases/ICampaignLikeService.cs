using DonationApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.UseCases
{
    public interface ICampaignLikeService
    {
        Task<int> LikeCampaignAsync(IModel model);
        Task<int> UnlikeCampaignAsync(IModel model);

        Task<bool> IsUserLikeCampaign(string userId, int campaignId);
    }
}
