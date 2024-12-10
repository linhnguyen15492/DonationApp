export interface CommentResponse {
  userName: string;
  content: string;
  userId: string;
  campaignId: number;
}

export interface CommentModel {
  content: string;
  campaignId: number;
  userId: string;
}
