
export interface AddArticleRequest{
    title: string;
    content: string;
    subCategoryId: string;
    imageUrl: string; 
    tagsIDs?: string[];
}