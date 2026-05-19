import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ImageService {
  // Backend base URL (change if your backend runs on a different port)
  private backendUrl = 'https://localhost:7259';

  getFullImageUrl(imageUrl: string): string {
    if (!imageUrl) return 'assets/images/default-product.svg';
    if (imageUrl.startsWith('/')) {
      return `${this.backendUrl}${imageUrl}`;
    }
    return imageUrl;
  }
}