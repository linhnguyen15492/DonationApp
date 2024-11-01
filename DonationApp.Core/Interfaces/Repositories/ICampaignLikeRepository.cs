using DonationApp.Core.Entities;

namespace DonationApp.Core.Interfaces.Repositories
{
    public interface ICampaignLikeRepository : IGenericRepository<CampaignLike>
    {
        Task<CampaignLike?> GetByUserIdAndCampaignId(string userId, int campaignId);
    }
}
