
export interface AddArticleRequest{
    title: string;
    content: string;
    subCategoryId: string;
    userId: string;
    imageUrl: string; 
    createdDate: Date;
    tagsIDs?: string[]; // List of tag IDs as strings
}