using System.ComponentModel.DataAnnotations.Schema;

namespace DonationApp.Core.Entities
{
    public class Comment : AuditEntity<int>
    {
        public int CampaignId { get; set; }

        [ForeignKey("CampaignId")]
        public Campaign? Campaign { get; set; }

        public string Content { get; set; } = string.Empty;

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
