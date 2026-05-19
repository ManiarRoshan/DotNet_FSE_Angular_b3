CREATE DATABASE day13_HandsOn
USE day13_HandsOn

CREATE TABLE brands (
    brand_id INT PRIMARY KEY,
    brand_name VARCHAR(50)
);

CREATE TABLE categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(50)
);

CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    brand_id INT,
    category_id INT,
    model_year INT,
    list_price DECIMAL(10, 2),
    FOREIGN KEY (brand_id) REFERENCES brands(brand_id),
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);

SELECT 
    p.product_name, 
    b.brand_name, 
    c.category_name, 
    p.model_year, 
    p.list_price
FROM products p
INNER JOIN brands b ON p.brand_id = b.brand_id
INNER JOIN categories c ON p.category_id = c.category_id

SELECT * FROM products WHERE list_price>500

SELECT * FROM products ORDER BY list_price