using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonationApp.Core.Entities
{
    [NotMapped]
    public class Rating : AuditEntity<int>
    {
        public int CampaignId { get; set; }

        public Campaign? Campaign { get; set; }

        [Range(1, 5)]
        public int Value { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
