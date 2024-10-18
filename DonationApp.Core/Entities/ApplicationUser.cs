using DonationApp.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace DonationApp.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string UserId => Id;
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public UserRoleEnum Role { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
