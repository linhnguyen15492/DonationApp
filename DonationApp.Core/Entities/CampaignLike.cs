using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Entities
{
    public class CampaignLike
    {
        public int CampaignId { get; set; }

        [ForeignKey("CampaignId")]
        public Campaign? Campaign { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
