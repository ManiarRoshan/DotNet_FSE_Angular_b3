

-- Test performance improvement using execution plan.

--Scenario
--The management team frequently accesses product and order summary reports. To simplify access and improve performance,
--they require database views and indexing.

USE day15_HandsOn
--p.products, b.brands, c.categories

-- Create a view that shows product name, brand name, category name, model year and list price.
GO
CREATE VIEW Vw_prods AS 
SELECT p.product_name,b.brand_name,c.category_name,p.model_year,p.list_price
FROM products p
JOIN brands b ON p.brand_id = b.brand_id
JOIN categories c ON p.category_id = c.category_id;
GO

select * from Vw_prods


-- Create a view that shows order details with customer name, store name and staff name.
CREATE TABLE staffs (
    staff_id INT PRIMARY KEY IDENTITY(1,1),
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    store_id INT FOREIGN KEY REFERENCES stores(store_id)
);

CREATE TABLE orders(
    order_id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT FOREIGN KEY REFERENCES customers(customer_id),
    store_id INT FOREIGN KEY REFERENCES stores(store_id),
    staff_id INT FOREIGN KEY REFERENCES staffs(staff_id),
    order_date DATE
);

-- Insert base data
INSERT INTO categories (category_name) VALUES ('Mountain Bikes');
INSERT INTO brands (brand_name) VALUES ('Trek');
INSERT INTO stores (store_name, city) VALUES ('Main Store', 'New York');

-- Insert children records with specific IDs (matching the ones above)
INSERT INTO products (product_name, brand_id, category_id, model_year, list_price) 
VALUES ('Fuel EX', 1, 1, 2024, 3000.00);

INSERT INTO customers (first_name, last_name, city, email) 
VALUES ('John', 'Doe', 'New York', 'john@example.com');

INSERT INTO staffs (first_name, last_name, store_id) 
VALUES ('Alice', 'Staff', 1);

-- Insert order using matching IDs from above
INSERT INTO orders (customer_id, store_id, staff_id, order_date) 
VALUES (1, 1, 1, GETDATE());


GO
CREATE VIEW vw_OrderSummary AS
SELECT 
    o.order_id,
    c.first_name + ' ' + c.last_name AS customer_name,
    s.store_name,
    st.first_name + ' ' + st.last_name AS staff_name
FROM orders o
JOIN customers c ON o.customer_id = c.customer_id
JOIN stores s ON o.store_id = s.store_id
JOIN staffs st ON o.staff_id = st.staff_id;
GO

select * from vw_OrderSummary


-- Create appropriate indexes on foreign key columns.

--Products_idx preformance
CREATE INDEX idx_brand_id ON products(brand_id)
CREATE INDEX idx_category_id ON products(category_id)

--order_idx preformance
CREATE INDEX idx_customer_id ON orders(customer_id)
CREATE INDEX idx_store_id ON orders(store_id)
CREATE INDEX idx_staff_id ON orders(staff_id)


SELECT * FROM vw_OrderSummary WHERE customer_name = 'John Doe'


