using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.Models
{
    public class TokenModel
    {
        public string UserId { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty; // jwt token
        public string RefreshToken { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        public string Roles { get; set; } = string.Empty;
    }
}
