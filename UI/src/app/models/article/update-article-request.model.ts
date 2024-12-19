
export interface UpdateArticleRequest{
    title: string;
    content: string;
    subCategoryId: string;
    imageUrl: string; 
    updatedDate: string;
    userId: string;
    //createDate: Date;
    deletedTags?: string[];
    tagsIDs?: string[];
    tags: { name: string; id: string }[];
}