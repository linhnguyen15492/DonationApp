using DonationApp.Core.Entities;
using DonationApp.Core.Enums;
using DonationApp.Core.Interfaces;
using DonationApp.UseCase.Models;
using DonationApp.UseCase.UseCases;

namespace DonationApp.Infrastructure.Services
{
    public class TransferManager : ITransferManager
    {
        private readonly ITransactionUnitOfWork _transactionUnitOfWork;

        public TransferManager(ITransactionUnitOfWork transactionUnitOfWork)
        {
            _transactionUnitOfWork = transactionUnitOfWork ?? throw new ArgumentNullException(nameof(_transactionUnitOfWork));
        }

        public async Task<TransactionResult> DonateAsync(IModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                var input = (TransferModel)model;

                try
                {
                    await _transactionUnitOfWork.BeginTransactionAsync();

                    UserAccount? fromAccount = await _transactionUnitOfWork.UserAccountRepository.FindByIdAsync(input.FromAccountNumber);
                    CampaignAccount? toAccount = await _transactionUnitOfWork.CampaignAccountRepository.FindByIdAsync(input.ToAccountNumber);

                    if (fromAccount == null || fromAccount.IsLocked)
                    {
                        return TransactionResult.SourceNotFound;
                    }

                    var balanceLeft = fromAccount.Balance - input.Amount;

                    if (balanceLeft < fromAccount.MinimumRequiredAmount)
                    {
                        return TransactionResult.BalanceTooLow;
                    }

                    if (toAccount == null || toAccount.IsLocked)
                    {
                        return TransactionResult.DestinationNotFound;
                    }

                    fromAccount.Balance -= input.Amount;
                    await _transactionUnitOfWork.UserAccountRepository.UpdateAsync(fromAccount);

                    await _transactionUnitOfWork.TransactionRepository.InsertAsync(new Transaction()
                    {
                        Amount = input.Amount,
                        AccountNumber = fromAccount.AccountNumber,
                        BankAccountId = fromAccount.Id,
                        TransactionDate = DateTime.UtcNow,
                        TransactionType = TransactionTypeEnum.Withdrawal,
                        Notes = $"Transfer to {toAccount.Id}"
                    });

                    toAccount.Balance += input.Amount;
                    await _transactionUnitOfWork.CampaignAccountRepository.UpdateAsync(toAccount);
                    await _transactionUnitOfWork.TransactionRepository.InsertAsync(new Transaction()
                    {
                        Amount = input.Amount,
                        AccountNumber = toAccount.AccountNumber,
                        BankAccountId = fromAccount.Id,
                        TransactionDate = DateTime.UtcNow,
                        TransactionType = TransactionTypeEnum.Deposit,
                        Notes = $"Transfer from {fromAccount.Id}"
                    });

                    await _transactionUnitOfWork.SaveChangesAsync();

                    return TransactionResult.Success;
                }
                catch (Exception ex)
                {
                    return new TransactionResult(TransactionResultCodes.Error, ex.Message);
                }
            }
        }

        public async Task<TransactionResult> DisburseAsync(IModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                var input = (TransferModel)model;

                try
                {
                    await _transactionUnitOfWork.BeginTransactionAsync();

                    CampaignAccount? fromAccount = await _transactionUnitOfWork.CampaignAccountRepository.FindByIdAsync(input.FromAccountNumber);
                    UserAccount? toAccount = await _transactionUnitOfWork.UserAccountRepository.FindByIdAsync(input.ToAccountNumber);

                    if (fromAccount == null || fromAccount.IsLocked)
                    {
                        return TransactionResult.SourceNotFound;
                    }

                    var balanceLeft = fromAccount.Balance - input.Amount;

                    if (balanceLeft < fromAccount.MinimumRequiredAmount)
                    {
                        return TransactionResult.BalanceTooLow;
                    }

                    if (toAccount == null || toAccount.IsLocked)
                    {
                        return TransactionResult.DestinationNotFound;
                    }

                    fromAccount.Balance -= input.Amount;
                    await _transactionUnitOfWork.CampaignAccountRepository.UpdateAsync(fromAccount);

                    await _transactionUnitOfWork.TransactionRepository.InsertAsync(new Transaction()
                    {
                        Amount = input.Amount,
                        AccountNumber = fromAccount.AccountNumber,
                        BankAccountId = fromAccount.Id,
                        TransactionDate = DateTime.UtcNow,
                        TransactionType = TransactionTypeEnum.Withdrawal,
                        Notes = $"Transfer to {toAccount.Id}"
                    });

                    toAccount.Balance += input.Amount;
                    await _transactionUnitOfWork.UserAccountRepository.UpdateAsync(toAccount);
                    await _transactionUnitOfWork.TransactionRepository.InsertAsync(new Transaction()
                    {
                        Amount = input.Amount,
                        AccountNumber = toAccount.AccountNumber,
                        BankAccountId = fromAccount.Id,
                        TransactionDate = DateTime.UtcNow,
                        TransactionType = TransactionTypeEnum.Deposit,
                        Notes = $"Transfer from {fromAccount.Id}"
                    });

                    await _transactionUnitOfWork.SaveChangesAsync();

                    return TransactionResult.Success;
                }
                catch (Exception ex)
                {
                    return new TransactionResult(TransactionResultCodes.Error, ex.Message);
                }
            }
        }
    }
}
