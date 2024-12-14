
export interface UpdateArticleRequest{
    title: string;
    content: string;
    subCategoryId: string;
    userId: string;
    imageUrl: string; 
    //updatedDate: Date;
    //createDate: Date;
    deletedTags?: string[];
    tagsIDs?: string[];
    
    tags: { name: string; id: string }[];
}