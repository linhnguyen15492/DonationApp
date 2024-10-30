using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.Models
{
    public class CampaignModel : IModel
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public string OrganizationId { get; set; } = string.Empty;
    }
}
