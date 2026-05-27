import {
  filterCatalogProducts,
  getActiveProducts,
  isProductDeleted,
  isSearchActive,
  sortAdminProducts
} from './product.utils';
import { Product } from '../models/product.model';

describe('product.utils', () => {
  const products: Product[] = [
    { productId: 1, name: 'Apple', description: 'Fruit', price: 100, stock: 5, imageUrl: '', category: 'Food', isDeleted: true },
    { productId: 2, name: 'Camera', description: 'Security', price: 5000, stock: 4, imageUrl: '', category: 'Smart Home' },
    { productId: 3, name: 'Bulb', description: 'Smart bulb', price: 999, stock: 10, imageUrl: '', category: 'Smart Home', isDeleted: false }
  ];

  it('isProductDeleted should detect soft-deleted items', () => {
    expect(isProductDeleted(products[0])).toBe(true);
    expect(isProductDeleted(products[1])).toBe(false);
  });

  it('getActiveProducts should exclude deleted items', () => {
    expect(getActiveProducts(products).length).toBe(2);
  });

  it('filterCatalogProducts should hide deleted when not searching', () => {
    const result = filterCatalogProducts(products, {});
    expect(result.map(p => p.name)).toEqual(['Camera', 'Bulb']);
  });

  it('filterCatalogProducts should include deleted when searching', () => {
    const result = filterCatalogProducts(products, { searchTerm: 'apple' });
    expect(result.length).toBe(1);
    expect(result[0].name).toBe('Apple');
  });

  it('isSearchActive should trim whitespace', () => {
    expect(isSearchActive('  hi ')).toBe(true);
    expect(isSearchActive('   ')).toBe(false);
  });

  it('sortAdminProducts should place deleted items last', () => {
    const sorted = sortAdminProducts(products);
    expect(sorted[sorted.length - 1].name).toBe('Apple');
  });
});
