using DonationApp.Core.Interfaces;
using DonationApp.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.UseCases
{
    public interface ICampaignService
    {
        Task<Result<IDto>> CreateCampaignAsync(IModel model);
        Task<Result<IDto>> UpdateCampaignAsync(IModel model);
        Task<Result<IDto>> GetCampaignByIdAsync(object id);
        Task<Result<IEnumerable<IDto>>> GetAllCampaignsAsync();
    }
}
