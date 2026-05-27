import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ImageService {
  // Gateway base URL (without /api)
  private gatewayBaseUrl = environment.apiUrl.replace('/api', '');

  getFullImageUrl(imageUrl: string): string {
    if (!imageUrl) return `${this.gatewayBaseUrl}/images/default-product.svg`;
    if (imageUrl.startsWith('http')) return imageUrl;
    // Ensure the path starts with /images/
    let path = imageUrl;
    if (!path.startsWith('/images/')) {
      path = `/images/${path}`;
    }
    return `${this.gatewayBaseUrl}${path}`;
  }
}