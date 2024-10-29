using DonationApp.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonationApp.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public double Amount { get; set; }

        public required string AccountNumber { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public TransactionTypeEnum TransactionTypeEnum { get; set; }

        public int BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public BankAccount BankAccount { get; set; } = default!;

        public Guid ReferenceId { get; set; }
    }
}
