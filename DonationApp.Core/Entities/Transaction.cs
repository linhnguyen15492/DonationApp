using DonationApp.Core.Enums;
using DonationApp.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonationApp.Core.Entities
{
    public class Transaction : EntityBase<int>
    {
        public double Amount { get; set; }

        public required string AccountNumber { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public TransactionTypeEnum TransactionType { get; set; }

        public int BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public BankAccount BankAccount { get; set; } = default!;

        public Guid ReferenceId { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
