CREATE DATABASE day13_HandsOn
USE day13_HandsOn

-- Create Brands table
CREATE TABLE brands (
    brand_id INT PRIMARY KEY,
    brand_name VARCHAR(50)
);

-- Create Categories table
CREATE TABLE categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(50)
);

-- Create Products table
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
