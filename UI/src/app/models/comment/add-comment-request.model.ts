export interface AddCommentRequest{
    content: string;
    articleId: string;
    userId: string;
    createdDate: Date;
}