import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product, ProductDTO } from '../models/product.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private apiUrl = `${environment.apiUrl}/products`;
  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }
  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }
  addProduct(dto: ProductDTO): Observable<any> {
    console.log('POST to:', this.apiUrl, dto);
    return this.http.post(this.apiUrl, dto);
  }
  updateProduct(id: number, dto: ProductDTO): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, dto);
  }
  deleteProduct(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
  getProductsForAdmin(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/admin/all`);
  }
}