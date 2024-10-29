using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Entities
{
    public class SpecialBankAccount : BankAccount
    {
        public string BankName { get; set; } = string.Empty;
        public new double Balance { get; set; }
    }
}
