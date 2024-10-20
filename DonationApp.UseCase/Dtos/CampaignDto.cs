using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.Dtos
{
    public class CampaignDto : BaseDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string OrganizationId { get; set; } = string.Empty;

        public bool IsActivated { get; set; }

        public CampaignAccountDto? CampaignAccountDto { get; set; }
    }
}
