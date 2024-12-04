using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; } = string.Empty; // jwt token
        public string RefreshToken { get; set; } = string.Empty;
    }
}
