using DonationApp.Core.Enums;
using DonationApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DonationApp.Core.Entities
{
    public class ApplicationUser : IdentityUser, IAuditEntity<string>
    {
        public string UserId => Id;
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;

        public UserAccount? UserAccount { get; set; }
    }
}
