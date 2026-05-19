export interface Order {
  orderId: number;
  userId: number;
  orderDate: string;
  totalAmount: number;
  orderItems: OrderItem[];
}

export interface OrderItem {
  orderItemId: number;
  orderId: number;
  productId: number;
  quantity: number;
  price: number;
  product?: any;
}

export interface OrderDTO {
  userId: number;
  items: OrderItemDTO[];
}

export interface OrderItemDTO {
  productId: number;
  quantity: number;
}