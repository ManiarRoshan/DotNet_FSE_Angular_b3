CREATE DATABASE day15_HandsOn
USE day15_HandsOn

ALTER DATABASE OldDatabaseName MODIFY NAME = NewDatabaseName;

CREATE TABLE categories (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    category_name VARCHAR(255) NOT NULL
);

CREATE TABLE brands (
    brand_id INT PRIMARY KEY IDENTITY(1,1),
    brand_name VARCHAR(255) NOT NULL
);

CREATE TABLE products (
    product_id INT PRIMARY KEY IDENTITY(1,1),
    product_name VARCHAR(255) NOT NULL,
    brand_id INT FOREIGN KEY REFERENCES brands(brand_id),
    category_id INT FOREIGN KEY REFERENCES categories(category_id),
    model_year SMALLINT,
    list_price DECIMAL(10,2)
);

CREATE TABLE customers (
    customer_id INT PRIMARY KEY IDENTITY(1,1),
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    city VARCHAR(100),
    email VARCHAR(255)
);

CREATE TABLE stores (
    store_id INT PRIMARY KEY IDENTITY(1,1),
    store_name VARCHAR(255),
    city VARCHAR(100)
);

INSERT INTO categories VALUES ('Sedan'), ('SUV'), ('Hatchback'), ('Coupe'), ('Convertible');
INSERT INTO brands VALUES ('Toyota'), ('Honda'), ('Ford'), ('BMW'), ('Tesla');

INSERT INTO products VALUES 
('Camry', 1, 1, 2023, 25000), ('Civic', 2, 1, 2022, 22000),
('Explorer', 3, 2, 2023, 45000), ('X5', 4, 2, 2021, 60000),
('Model 3', 5, 1, 2024, 35000);

INSERT INTO customers VALUES 
('John', 'Doe', 'New York', 'john@email.com'), ('Jane', 'Smith', 'Chicago', 'jane@email.com'),
('Alice', 'Brown', 'New York', 'alice@email.com'), ('Bob', 'White', 'Austin', 'bob@email.com'),
('Charlie', 'Green', 'Chicago', 'charlie@email.com');

INSERT INTO stores VALUES ('AutoWorld NY', 'New York'), ('City Cars', 'Chicago'), 
('Texas Motors', 'Austin'), ('Elite Autos', 'Los Angeles'), ('North Store', 'Seattle');


--Create EcommDb and all tables using the provided schema.
-- Insert at least 5 records in categories, brands, products, customers, and stores.

-- Write SELECT queries to retrieve all products with their brand and category names.
SELECT p.product_name,c.category_name,b.brand_name FROM products p
JOIN categories c ON p.category_id=c.category_id
JOIN brands b ON p.brand_id=b.brand_id

-- Retrieve all customers from a specific city.
SELECT *,CONCAT(c.first_name,' ',c.last_name) AS Full_Name FROM customers c WHERE C.city='New York'

-- Display total number of products available in each category.
SELECT c.category_name,SUM(p.product_id) AS TOTAL_PRODUCTS FROM categories c
LEFT JOIN products p ON c.category_id=P.product_id
GROUP BY c.category_name

