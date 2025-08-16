export interface IProduct {
  id: number;
  name: string;
  description: string;
  price: number;
  imageUrl?: string;
  averageRating?: number;
  reviews?: number;
  brandId: number;
}
