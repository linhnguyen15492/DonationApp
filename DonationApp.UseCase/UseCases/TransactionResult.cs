using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.Services
{
    public class TransactionResult
    {
        public bool IsSuccess => ResultCode == TransactionResultCodes.Success;

        public TransactionResult(TransactionResultCodes resultCode, string message)
        {
            ResultCode = resultCode;
            Message = message ?? string.Empty;
        }

        public TransactionResultCodes ResultCode { get; }
        public string Message { get; }

        public string Sender { get; set; } = string.Empty;
        public string Receiver { get; set; } = string.Empty;

        public static readonly TransactionResult SourceNotFound = new(TransactionResultCodes.SourceNotFound, string.Empty);
        public static readonly TransactionResult DestinationNotFound = new(TransactionResultCodes.DestinationNotFound, string.Empty);
        public static readonly TransactionResult BalanceTooLow = new(TransactionResultCodes.BalanceTooLow, string.Empty);
        public static readonly TransactionResult CashNotAvailable = new(TransactionResultCodes.CashNotAvailable, string.Empty);
        public static readonly TransactionResult CashWithdrawalError = new(TransactionResultCodes.CashWithdrawalError, string.Empty);
        //public static readonly TransactionResult Success = new(TransactionResultCodes.Success, string.Empty);

        public static TransactionResult Success(string message, string sender, string receiver) => new TransactionResult(TransactionResultCodes.Success, message)
        {
            Sender = sender,
            Receiver = receiver
        };
    }
}
