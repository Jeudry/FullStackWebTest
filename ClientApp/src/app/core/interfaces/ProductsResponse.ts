export interface ProductsResponse {
  name: string,
  code: string,
  description?: string,
  price: number,
  stock: number,
  createdAt: Date,
  updatedAt?: Date
}
