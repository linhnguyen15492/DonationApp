using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using DonationApp.Core.Interfaces.Repositories;
using DonationApp.Core.Shared;
using DonationApp.Infrastructure.Mappers;
using DonationApp.UseCase.Models;
using DonationApp.UseCase.Repositories;
using DonationApp.UseCase.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly ICampaignAccountRepository _campaignAccountRepository;
        public CampaignService(ICampaignRepository campaignRepository, ICampaignAccountRepository campaignAccountRepository)
        {
            _campaignRepository = campaignRepository ?? throw new ArgumentNullException(nameof(campaignRepository));
            _campaignAccountRepository = campaignAccountRepository ?? throw new ArgumentNullException(nameof(campaignAccountRepository));
        }

        public async Task<Result<IDto>> CreateCampaignAsync(IDto dto)
        {
            var model = dto as CampaignModel;
            var campaign = model.ToCampaign();

            var data = await _campaignRepository.AddAsync(campaign!);

            if (data is not null)
            {
                return Result<IDto>.Success(data.ToCampaignDto());
            }

            return Result<IDto>.Failure("");

        }

        public Task<Result<IDto>> DeleteCampaignAsync(IDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<IDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<IDto>> GetCampaignByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IDto>> UpdateCampaignAsync(IDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
