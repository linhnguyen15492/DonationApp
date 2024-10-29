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
        private readonly IUnitOfWork _unitOfWork;
        public CampaignService(ICampaignRepository campaignRepository, ICampaignAccountRepository campaignAccountRepository, IUnitOfWork unitOfWork)
        {
            _campaignRepository = campaignRepository ?? throw new ArgumentNullException(nameof(campaignRepository));
            _campaignAccountRepository = campaignAccountRepository ?? throw new ArgumentNullException(nameof(campaignAccountRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<IDto>> CreateCampaignAsync(IModel model)
        {
            var data = model as CampaignModel;

            //var startDate = DateTime.ParseExact(data!.StartDate, "yyyy/MM/dd", null);
            //var endDate = DateTime.ParseExact(data!.EndDate, "yyyy/MM/dd", null);

            var campaign = data?.ToCampaign();

            if (campaign is not null)
            {
                var res = await InsertCampaignAsync(campaign);

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

        public Task<Result<IDto>> GetCampaignByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IDto>> UpdateCampaignAsync(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
