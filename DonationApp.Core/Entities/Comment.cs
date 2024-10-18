using System.ComponentModel.DataAnnotations.Schema;

namespace DonationApp.Core.Entities
{
    public class Comment : BaseEntity
    {
        public int ReliefOperationId { get; set; }

        [ForeignKey("ReliefOperationId")]
        public Campaign? ReliefOperation { get; set; }

        public string Content { get; set; } = string.Empty;

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
