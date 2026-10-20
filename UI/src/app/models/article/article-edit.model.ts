export interface ArticleEdit{
    id: string;
    title: string;
    slug: string;
    content: string;
    imageUrl: string;
    isPublished: boolean;
    categoryId: string;
    subCategoryId: string;
    tags: { name: string; id: string }[];
}
