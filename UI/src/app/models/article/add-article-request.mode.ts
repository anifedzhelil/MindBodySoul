
export interface AddArticleRequest{
    title: string;
    content: string;
    subCategoryId: string;
    imageUrl: string; 
    isPublished: boolean;
    tagsIDs?: string[];
}