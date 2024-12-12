using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.Dtos
{
    public class Subscribers
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string SubscribeStatus { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
    }
}
