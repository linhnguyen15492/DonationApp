using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Entities
{
    public class CampaignLikeCount
    {
        public int CampaignId { get; set; }

        public Campaign? Campaign { get; set; }

        public int Count { get; set; }
    }
}
