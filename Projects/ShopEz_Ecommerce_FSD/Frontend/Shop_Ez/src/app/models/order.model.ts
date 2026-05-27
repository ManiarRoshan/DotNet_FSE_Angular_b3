export interface Order {
  orderId: number;
  userId: number;
  orderDate: string;
  totalAmount: number;
  shippingAddress: string;
  paymentMethod: string;
  paymentStatus: string;
  orderStatus?: string;
  orderItems: OrderItem[];
}

export interface OrderItem {
  productId: number;
  productName: string;
  productImageUrl: string;
  price: number;
  quantity: number;
  subtotal: number;
}

export interface OrderDTO {
  userId: number;
  shippingAddress: string;
  paymentMethod: string;
  items: OrderItemDTO[];
}

export interface OrderItemDTO {
  productId: number;
  quantity: number;
}