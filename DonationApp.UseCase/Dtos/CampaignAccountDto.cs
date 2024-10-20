using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.Dtos
{
    public class CampaignAccountDto
    {
        public bool IsLocked { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
    }
}
