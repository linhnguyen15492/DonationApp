using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Entities
{
    public class CampaignAccount : BankAccount
    {
        public int CampaignId { get; set; }
        public Campaign? Campaign { get; set; }
    }
}
