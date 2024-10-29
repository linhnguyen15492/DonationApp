using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace DonationApp.Core.Entities
{
    public class Campaign : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string OrganizationId { get; set; } = string.Empty;

        [ForeignKey(nameof(OrganizationId))]
        public ApplicationUser? Organization { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public ICollection<Rating>? Ratings { get; set; }

        public bool IsActivated { get; set; }

        public CampaignAccount CampaignAccount { get; set; } = default!;
    }
}
