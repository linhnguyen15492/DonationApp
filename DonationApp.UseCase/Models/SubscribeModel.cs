using DonationApp.Core.Enums;
using DonationApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.Models
{
    public class SubscribeModel : IModel
    {
        public int CampaignId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
