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
                Name = campaign.Name,
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
                Comments = campaign.Comments?.Select(c => new CommentDto
                {
                    UserId = c.UserId!,
                    Content = c.Content,
                    CampaignId = c.CampaignId,
                    UserName = c.ApplicationUser?.FullName!
                }).ToList() ?? new List<CommentDto>()
            };
        }

        public static Campaign ToCampaign(this CampaignModel model)
        {
            return new Campaign
            {
                Name = model.Name,
                Description = model.Description,
                Location = model.Location,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                OrganizationId = model.OrganizationId,
            };
        }
    }
}
