CREATE TABLE Stores (
    store_id INT PRIMARY KEY IDENTITY(1,1),
    store_name VARCHAR(100)
);
CREATE TABLE Products (
    product_id INT PRIMARY KEY IDENTITY(1,1),
    product_name VARCHAR(100),
    model_year INT 
);
CREATE TABLE Sales (
    sale_id INT PRIMARY KEY IDENTITY(1,1),
    store_id INT,
    product_id INT,
    quantity INT,
    list_price DECIMAL(10,2),
    discount DECIMAL(4,2)
);
CREATE TABLE Stocks (
    store_id INT,
    product_id INT,
    quantity INT,
    PRIMARY KEY (store_id, product_id)
);

INSERT INTO Stores VALUES ('Downtown Bikes'), ('Uptown Cycles');

INSERT INTO Products VALUES ('Mountain Pro', 2015), ('Road Racer', 2024);

INSERT INTO Sales (store_id, product_id, quantity, list_price, discount)
VALUES (1, 1, 5, 500.00, 0.10), (2, 2, 2, 1000.00, 0.05);

INSERT INTO Stocks VALUES (1, 1, 0), (1, 2, 15), (2, 2, 8);


--1. Identify products sold in each store using nested queries.
SELECT DISTINCT store_id,product_id,(SELECT product_name FROM Products P WHERE P.product_id = S.product_id) AS ProductName 
FROM Sales S;

--2. Compare sold products with current stock using INTERSECT and EXCEPT operators.

--3. Display store_name, product_name, total quantity sold.

--4. Calculate total revenue per product (quantity × list_price – discount).

--5. Update stock quantity to 0 for discontinued products (simulation).
