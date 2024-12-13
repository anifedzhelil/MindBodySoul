import { Tag } from '../tag/tag.model';

export interface ArticleDetails {
  id: string;
  title: string;
  content: string;
  subCategoryId: string;
  userId: string;
  imageUrl: string;
  createdDate: Date;
  updatedDate: Date;
  tagsIDs: Tag[];
} 
