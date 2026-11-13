export interface ArticleComment {
    id: string;
    content: string;
    articleId: string;
    userId: string;
    createdDate: Date;
    userName: string | null;
    updatedDate: Date | null;
}