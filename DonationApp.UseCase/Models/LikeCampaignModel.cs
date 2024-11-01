using DonationApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.Models
{
    public class LikeCampaignModel : IModel
    {
        public int CampaignId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
