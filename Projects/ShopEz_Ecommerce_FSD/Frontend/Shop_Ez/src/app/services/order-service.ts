import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order, OrderDTO } from '../models/order.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class OrderService {
  private apiUrl = `${environment.apiUrl}/orders`;
  constructor(private http: HttpClient) { }

  createOrder(dto: OrderDTO): Observable<Order> { 
    return this.http.post<Order>(this.apiUrl, dto); 
  }
  getAllOrders(): Observable<Order[]> { 
    return this.http.get<Order[]>(this.apiUrl); 
  }
  getOrderById(id: number): Observable<Order> {
     return this.http.get<Order>(`${this.apiUrl}/${id}`); 
    }
  getMyOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/myorders`);
  }
  cancelOrder(id: number): Observable<Order> {
    return this.http.put<Order>(`${this.apiUrl}/${id}/cancel`, {});
  }
}