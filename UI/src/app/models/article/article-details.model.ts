
export interface ArticleDetails {
  id: string;
  title: string;
  content: string;
  subCategoryId: string;
  categoryName: string;
  subCategoryName: string;
  categoryId: string;
  userId: string;
  imageUrl: string;
  createdDate: string;
  updatedDate: string;
  tags: { name: string; id: string }[];
}
