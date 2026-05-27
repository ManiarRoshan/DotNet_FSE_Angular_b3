import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting, HttpTestingController } from '@angular/common/http/testing';
import { AdminDashboardService } from './admin-dashboard.service';
import { environment } from '../../environments/environment';

describe('AdminDashboardService', () => {
  let service: AdminDashboardService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()]
    });
    service = TestBed.inject(AdminDashboardService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('preload should cache products and orders', () => {
    service.preload(true);

    const productsReq = httpMock.expectOne(`${environment.apiUrl}/products/admin/all`);
    productsReq.flush([{ productId: 1, name: 'Test', description: 'd', price: 10, stock: 1, imageUrl: '', category: 'X' }]);

    const ordersReq = httpMock.expectOne(`${environment.apiUrl}/orders`);
    ordersReq.flush([]);

    expect(service.snapshot?.products.length).toBe(1);
    expect(service.snapshot?.orders).toEqual([]);
  });
});
