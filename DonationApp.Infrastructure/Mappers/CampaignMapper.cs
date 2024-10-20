using DonationApp.Core.Entities;
using DonationApp.UseCase.Dtos;
using DonationApp.UseCase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                IsActivated = campaign.IsActivated,
                IsDeleted = campaign.IsDeleted,
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
