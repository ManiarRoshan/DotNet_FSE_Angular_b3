// A reusable function to get the first item of any array type-safely
function getFirstElement<T>(items: T[]): T {
    return items[0];
}

// Use Case Implementation
// Defining specific models
interface User {
  id: number;
  name: string;
}

interface Product {
  id: number;
  title: string;
}
//  Generic Interface
// Defines a contract for any repository-style class
interface Repository<T>{
  add(item: T): void;
  getAll(): T[];
}

//  Generic Class
// Implements the Repository interface to manage a collection of type T
class DataManager<T> implements Repository<T> {
  private data: T[] = [];

  add(item: T): void {
    this.data.push(item);
  }

  getAll(): T[] {
    return [...this.data]; // Returns a shallow copy for immutability
  }
}

// --- Testing with Users ---
const userManager = new DataManager<User>();
userManager.add({ id: 1, name: "Alice"});
userManager.add({ id: 2, name: "Bob"});

const users = userManager.getAll();
console.log("Users List:", users);
console.log("First User:", getFirstElement(users));

// --- Testing with Products ---
const productManager = new DataManager<Product>();
productManager.add({id: 101,title: "Mechanical Keyboard"});
productManager.add({id: 102,title: "Wireless Mouse"});

const products = productManager.getAll();
console.log("Products List:", products);
console.log("First Product:", getFirstElement(products));
