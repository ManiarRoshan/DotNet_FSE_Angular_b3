import { Product } from '../models/product.model';

export function isProductDeleted(product: Product): boolean {
  return !!product.isDeleted;
}

export function isSearchActive(searchTerm?: string): boolean {
  return !!searchTerm?.trim();
}

export function getActiveProducts(products: Product[]): Product[] {
  return products.filter(p => !isProductDeleted(p));
}

export function matchesCategory(product: Product, category: string): boolean {
  return (
    (!!product.category && product.category === category) ||
    product.name.toLowerCase().includes(category.toLowerCase()) ||
    product.description.toLowerCase().includes(category.toLowerCase())
  );
}

/** Browse lists hide deleted items; search may include them (shown as unavailable). */
export function filterCatalogProducts(
  products: Product[],
  options: {
    searchTerm?: string;
    category?: string;
    includeAllCategories?: boolean;
  }
): Product[] {
  let list = [...products];
  const searching = isSearchActive(options.searchTerm);

  if (!searching) {
    list = list.filter(p => !isProductDeleted(p));
  }

  const category = options.category?.trim();
  if (category && category !== 'All' && !options.includeAllCategories) {
    list = list.filter(p => matchesCategory(p, category));
  }

  if (searching) {
    const term = options.searchTerm!.trim().toLowerCase();
    list = list.filter(
      p =>
        p.name.toLowerCase().includes(term) ||
        p.description.toLowerCase().includes(term)
    );
  }

  return list;
}

export function sortAdminProducts(products: Product[]): Product[] {
  return [...products].sort((a, b) => {
    const aDeleted = isProductDeleted(a) ? 1 : 0;
    const bDeleted = isProductDeleted(b) ? 1 : 0;
    if (aDeleted !== bDeleted) return aDeleted - bDeleted;
    return (b.productId ?? 0) - (a.productId ?? 0);
  });
}
