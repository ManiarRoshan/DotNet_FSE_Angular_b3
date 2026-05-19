/*Level - 2 Problem 1: Online Shopping Cart System
Scenario:
An e-commerce platform needs a flexible cart system where different product types calculate discounts differently.
Requirements:
1. Create a base class Product with properties Name and Price.
2. Create derived classes Electronics and Clothing.
3. Implement a virtual method CalculateDiscount().
4. Electronics get 5% discount, Clothing gets 15% discount.
5. Use encapsulation to protect price updates.
Technical Constraints:
• Use private fields with public properties.
• Apply inheritance and method overriding.
• Prevent negative price assignment.
Expectations:
• Demonstrate polymorphic behavior in cart processing.
• Apply data validation inside properties.
• Calculate and display final price after discount.
Learning Outcome:
• Combine encapsulation and polymorphism.
• Design extensible product hierarchy.
• Implement business logic in overridden methods.
Sample Input: Electronics Price = 20000
Sample Output: Final Price after 5% discount = 19000
*/

using System;
using System.Collections.Generic;
namespace ShoppingCart
{
    public class Product
    {
        private double _price;
        public string Name { get; set; }
        public double Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative");
                    
                }
                _price = value;
            }

        }
        public Product(string name,double price)
        {
            Name = name;
            Price = price;
        }
        public virtual double CalculateDiscount()
        {
            return 0;
        }

        public double GetFinalPrice()
        {
            return Price - CalculateDiscount();
        }
    }
    public class Electronics : Product
    {
        public Electronics(string name, double price) : base(name, price) { }

        public override double CalculateDiscount()
        {
            return Price * 0.05;
        }
    }

    public class Clothing : Product
    {
        public Clothing(string name, double price) : base(name, price) { }

        public override double CalculateDiscount()
        {
            return Price * 0.15;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> cart = new List<Product>
            {
                new Electronics("Laptop", 20000),
                new Clothing("Winter Jacket", 5000)
            };

            foreach (var item in cart)
            {
                Console.WriteLine($"Item: {item.Name}");
                Console.WriteLine($"Original Price: {item.Price}");
                Console.WriteLine($"Discount Applied: {item.CalculateDiscount()}");
                Console.WriteLine($"Final Price: {item.GetFinalPrice()}");
                Console.WriteLine("----------------------------------");
            }
        }
    }
}





