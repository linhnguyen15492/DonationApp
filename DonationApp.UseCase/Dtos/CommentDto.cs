using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.Dtos
{
    public class CommentDto
    {
        public int CampaignId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
