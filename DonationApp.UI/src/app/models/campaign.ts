import { CommentResponse } from './comment';

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
  comments: CommentResponse[];
  likeCount: number;
}

export interface CreateCampaign {
  name: string;
  description: string;
  location: string;
  startDate: Date;
  endDate: Date;
  organizationId: string;
}
