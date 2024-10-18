using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonationApp.Core.Entities
{
    public class Rating : BaseEntity
    {
        public int ReliefOperationId { get; set; }

        public Campaign? ReliefOperation { get; set; }

        [Range(1, 5)]
        public int Value { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
