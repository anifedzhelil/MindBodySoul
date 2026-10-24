export interface AdminArticleListItem {
  id: string;
  title: string;
  slug: string;
  imageUrl: string;
  isPublished: boolean;
  isArchived: boolean;
  createdDate: string;
  publishedDate: string | null;
  updatedDate: string | null;
  totalVisitCount: number;
  uniqueVisitCount: number;
}