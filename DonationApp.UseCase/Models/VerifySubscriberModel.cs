using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.Models
{
    public class VerifySubscriberModel
    {
        public string UserId { get; set; } = string.Empty;
        public int CampaignId { get; set; }
    }
}
