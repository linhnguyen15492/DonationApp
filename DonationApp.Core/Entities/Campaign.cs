using System.ComponentModel.DataAnnotations.Schema;

namespace DonationApp.Core.Entities
{
    public class Campaign : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string OrganizationId { get; set; } = string.Empty;

        [ForeignKey(nameof(OrganizationId))]
        public ApplicationUser? Organization { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public ICollection<Rating>? Ratings { get; set; }

        public bool IsActivated { get; set; }
    }
}
