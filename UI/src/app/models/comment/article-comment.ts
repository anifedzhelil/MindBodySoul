export interface ArticleComment{
    id: string;
    content: string;
    articleId: string;
    userId: string;
    createdDate: Date;
    userName: string;
    updatedDate: Date;
}