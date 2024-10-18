using System.ComponentModel.DataAnnotations.Schema;

namespace DonationApp.Core.Entities
{
    public class UserAccount : BankAccount
    {
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
