using DonationApp.Core.Entities;
using DonationApp.UseCase.Dtos;
using DonationApp.UseCase.Models;

namespace DonationApp.Infrastructure.Mappers
{
    public static class CampaignMapper
    {
        public static CampaignDto ToCampaignDto(this Campaign campaign)
        {
            return new CampaignDto
            {
                Id = campaign.Id,
                Title = campaign.Title,
                Description = campaign.Description,
                Location = campaign.Location,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate,
                OrganizationId = campaign.OrganizationId,
                OrganizationName = campaign.Organization?.FullName,
                IsActivated = campaign.IsActivated,
                IsDeleted = campaign.IsDeleted,
                AccountNumber = campaign.CampaignAccount?.AccountNumber,
                AccountBalance = campaign.CampaignAccount?.Balance,
            };
        }

        public static Campaign ToCampaign(this CampaignModel model)
        {
            return new Campaign
            {
                Title = model.Title,
                Description = model.Description,
                Location = model.Location,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                OrganizationId = model.OrganizationId,
            };
        }
    }
}
