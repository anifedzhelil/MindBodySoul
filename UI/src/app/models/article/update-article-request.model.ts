
export interface UpdateArticleRequest{
    title: string;
    content: string;
    subCategoryId: string;
    imageUrl: string; 
    deletedTags?: string[];
    tagsIDs?: string[];
    tags: { name: string; id: string }[];
}