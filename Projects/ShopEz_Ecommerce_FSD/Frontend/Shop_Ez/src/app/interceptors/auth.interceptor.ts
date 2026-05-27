import { HttpRequest, HttpHandlerFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

export function authInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn) {
  const router = inject(Router);
  const auth = inject(AuthService);
  const token = auth.getToken();

  const isPublic =
    req.url.includes('/api/auth/login') ||
    req.url.includes('/api/auth/register') ||
    // Only public product GET endpoints (exclude admin catalog)
    (req.method === 'GET' && req.url.includes('/api/products') && !req.url.includes('/api/products/admin')) ||
    // Static images are public
    (req.method === 'GET' && req.url.includes('/images/'));

  let clonedReq = req;
  if (token && !isPublic) {
    clonedReq = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
  }

  return next(clonedReq).pipe(
    catchError((err: HttpErrorResponse) => {
      console.error('[Interceptor] HTTP Error:', err.status, req.url);

      // Only logout if 401 and NOT a public endpoint AND token exists (to avoid infinite loop)
      if (err.status === 401 && !isPublic && token) {
        console.warn('[Interceptor] 401 on protected endpoint – logging out');
        auth.logout();
        router.navigate(['/login']);
      }
      return throwError(() => err);
    })
  );
}
