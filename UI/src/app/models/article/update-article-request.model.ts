
export interface UpdateArticleRequest{
    title: string;
    slug: string;
    content: string;
    subCategoryId: string;
    imageUrl: string; 
    isPublished: boolean;
    deletedTags?: string[];
    tagsIDs?: string[];
    tags: { name: string; id: string }[];
}