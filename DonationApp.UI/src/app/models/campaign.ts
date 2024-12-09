export interface Campaign {
  name: string;
  description: string;
  location: string;
  startDate: Date;
  endDate: Date;
  organizationId: string;
  organizationName: string;
  isActivated: boolean;
  accountNumber: string;
  accountBalance: number;
  id: number;
  isDeleted: boolean;
  comments: Comment[];
}
