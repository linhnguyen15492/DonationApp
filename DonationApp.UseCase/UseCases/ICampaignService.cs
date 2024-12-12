using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using DonationApp.Core.Shared;

namespace DonationApp.UseCase.UseCases
{
    public interface ICampaignService
    {
        Task<Result<IDto>> CreateCampaignAsync(IModel model);
        Task<Result<IDto>> UpdateCampaignAsync(IModel model);
        Task<Result<IDto>> GetCampaignByIdAsync(int id);
        Task<Result<IEnumerable<IDto>>> GetAllCampaignsAsync();
        Task<Result<IEnumerable<IDto>>> GetAllCampaignByUserId(string userId);
        Task<bool> SubscribeCampaign(IModel model);
        Task<bool> IsSubscribedCampaign(IModel model);
        Task<bool> DeactivateCampaign(int id);
        Task<bool> ActivateCampaign(int id);

    }
}
