
export interface AddArticleRequest{
    title: string;
    slug: string;
    content: string;
    subCategoryId: string;
    imageUrl: string; 
    isPublished: boolean;
    tagsIDs?: string[];
}