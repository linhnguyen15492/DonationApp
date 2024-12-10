using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using DonationApp.Core.Interfaces.Repositories;
using DonationApp.Core.Shared;
using DonationApp.Infrastructure.Mappers;
using DonationApp.UseCase.Models;
using DonationApp.UseCase.Repositories;
using DonationApp.UseCase.UseCases;
using System.Security.Principal;

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

        public async Task<Result<IDto>> CreateCampaignAsync(IModel model)
        {
            var data = model as CampaignModel;

            var campaign = data?.ToCampaign();

            if (campaign is not null)
            {
                await InsertCampaignAsync(campaign);

                var bankAccount = new CampaignAccount()
                {
                    CampaignId = campaign.Id,
                };

                await _campaignAccountRepository.InsertAsync(bankAccount);
                await _campaignAccountRepository.SaveAsync();


                return Result<IDto>.Success(campaign.ToCampaignDto());
            }
            else return Result<IDto>.Failure("Failed to create campaign");
        }


        private async Task<int> InsertCampaignAsync(Campaign campaign)
        {
            try
            {
                var data = await _campaignRepository.InsertAsync(campaign);
                await _campaignRepository.SaveAsync();

                return data.Id;

            }
            catch (Exception e) { throw new Exception(e.Message); }
        }


        public async Task<Result<IEnumerable<IDto>>> GetAllCampaignsAsync()
        {

            var data = await _campaignRepository.GetAllAsync();

            if (data is not null)
            {
                return Result<IEnumerable<IDto>>.Success(data.Select(c => c.ToCampaignDto()));
            }
            else return Result<IEnumerable<IDto>>.Failure("Failed to get all campaigns");

        }

        public async Task<Result<IEnumerable<IDto>>> GetAllCampaignByUserId(string userId)
        {

            var data = await _campaignRepository.GetAllCampaignByUserId(userId);

            if (data is not null)
            {
                return Result<IEnumerable<IDto>>.Success(data.Select(c => c.ToCampaignDto()));
            }
            else return Result<IEnumerable<IDto>>.Failure("Failed to get all campaigns");

        }

        public async Task<Result<IDto>> GetCampaignByIdAsync(int id)
        {
            var data = await _campaignRepository.GetByIdAsync(id);
            if (data is not null)
            {
                return Result<IDto>.Success(data.ToCampaignDto());
            }
            else return Result<IDto>.Failure("Failed to get campaign by id");

        }

        public Task<Result<IDto>> UpdateCampaignAsync(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
