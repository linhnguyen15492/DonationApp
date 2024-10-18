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
        Task<Result<IDto>> CreateCampaignAsync(IDto dto);
        Task<Result<IDto>> UpdateCampaignAsync(IDto dto);
        Task<Result<IDto>> DeleteCampaignAsync(IDto dto);
        Task<Result<IDto>> GetCampaignByIdAsync(int id);
        Task<Result<IEnumerable<IDto>>> GetAllAsync();
    }
}
