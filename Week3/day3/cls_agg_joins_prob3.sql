USE day13_HandsOn
CREATE TABLE stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100));
CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    store_id INT,
    order_status INT,
    FOREIGN KEY (store_id) REFERENCES stores(store_id));
CREATE TABLE order_items (
    order_id INT,
    item_id INT,
    product_id INT,
    quantity INT,
    list_price DECIMAL(10, 2),
    discount DECIMAL(4, 2),
    PRIMARY KEY (order_id, item_id),
    FOREIGN KEY (order_id) REFERENCES orders(order_id));

SELECT * FROM orders

INSERT INTO stores VALUES (1,'Downtown Bikes'), (2,'Suburban Sport');

INSERT INTO orders VALUES (501,1,4), (502,1,4), (503,2,4), (504,2,1);

INSERT INTO order_items VALUES 
(501, 1, 10, 2, 100.00, 0.10), 
(502, 1, 11, 1, 500.00, 0.00),
(503, 1, 12, 1, 1000.00, 0.20),
(504, 1, 13, 5, 200.00, 0.05); 

--1. Display store_name and total sales amount.
SELECT s.store_name,SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS Total_sales FROM stores s
JOIN orders o ON s.store_id= o.store_id
JOIN order_items oi ON o.order_id=oi.order_id
GROUP BY s.store_name

--2. Calculate total sales using (quantity * list_price * (1 - discount)).
SELECT SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS Total_sales FROM order_items oi


--3. Include only completed orders (order_status = 4).
SELECT s.store_name,SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS Total_sales FROM stores s
JOIN orders o ON s.store_id= o.store_id
JOIN order_items oi ON o.order_id=oi.order_id
WHERE o.order_status=4
GROUP BY s.store_name

--4. Group results by store_name.
SELECT s.store_name,SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS Total_sales FROM stores s
JOIN orders o ON s.store_id= o.store_id
JOIN order_items oi ON o.order_id=oi.order_id
WHERE o.order_status=4
GROUP BY s.store_name

--5. Sort total sales in descending order.
select SUM(oi.quantity*oi.list_price) AS Total_sales FROM order_items oi ORDER BY Total_sales desc

SELECT 
    s.store_name, 
    SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_sales
FROM stores s
INNER JOIN orders o ON s.store_id = o.store_id
INNER JOIN order_items oi ON o.order_id = oi.order_id
WHERE o.order_status = 4
GROUP BY s.store_name
ORDER BY total_sales DESC;
