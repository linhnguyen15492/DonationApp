export interface TransferModel {
  fromAccountNumber: string;
  toAccountNumber: string;
  amount: number;
  note: string;
  type: number;
  sender: string;
  receiver: string;
}
