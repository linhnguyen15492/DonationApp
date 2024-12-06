using DonationApp.Core.Enums;
using DonationApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.Models
{
    public class TransferModel : IModel
    {
        public string FromAccountNumber { get; set; } = string.Empty;
        public string ToAccountNumber { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string Notes { get; set; } = string.Empty;

        public TransferTypeEnum TransferType { get; set; }

        public string Sender { get; set; } = string.Empty;
        public string Receiver { get; set; } = string.Empty;
    }
}
