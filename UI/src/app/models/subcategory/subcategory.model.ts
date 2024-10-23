import { Category } from "../category/category.model";

export interface SubCategory{
    id: string;
    categoryId: string;
    name: string;
    urlHandle: string;
    icon: string;
    category: Category;
}