using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Entities
{
    public class CampaignLikeCount : EntityBase<int>
    {
        public int CampaignId { get; set; }

        [ForeignKey(nameof(CampaignId))]
        public Campaign? Campaign { get; set; }

        public int Count { get; set; }
    }
}
