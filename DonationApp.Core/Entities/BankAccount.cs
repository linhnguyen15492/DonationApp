using DonationApp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Entities
{
    public abstract class BankAccount : AuditEntity<int>
    {
        private double _balance;
        public double Balance
        {
            get { return _balance; }

            set
            {
                if (value < MinimumRequiredAmount)
                {
                    throw new InvalidBalanceException();
                }

                _balance = value;
            }
        }

        protected BankAccount()
        {
            Balance = 0;
            AccountNumber = GenerateAccountNumber();
        }

        public bool IsLocked { get; set; }

        public string AccountNumber { get; set; }

        protected string GenerateAccountNumber()
        {
            Random generator = new Random();
            string r = generator.Next(0, 1000000).ToString("D6");

            return r;
        }

        public double MinimumRequiredAmount { get; set; } // can be negative
    }
}
