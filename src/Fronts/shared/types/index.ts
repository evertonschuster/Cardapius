export interface User {
  id: string;
  name: string;
}

export interface Product {
  id: string;
  name: string;
  quantity: number;
}

export interface Order {
  id: string;
  items: Product[];
}
