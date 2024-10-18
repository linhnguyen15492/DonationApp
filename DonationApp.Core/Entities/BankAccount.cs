using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Entities
{
    public abstract class BankAccount : BaseEntity
    {
        private double _balance;
        public double Balance
        {
            get { return _balance; }

            private set
            {
                if (value < 0)
                {
                    throw new Exception("Balance cannot be negative");
                }

                _balance = value;
            }
        }

        public bool IsLocked { get; set; }

        [Required]
        public required string AccountNumber { get; set; } = string.Empty;
    }
}
