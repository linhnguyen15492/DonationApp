export interface TransferModel {
  fromAccountNumber: string;
  toAccountNumber: string;
  amount: number;
  note: string;
  transferType: number;
  sender: string;
  receiver: string;
}
