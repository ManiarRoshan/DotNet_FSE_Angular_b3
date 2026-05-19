CREATE DATABASE day14_HandsOn
USE day14_HandsOn

CREATE TABLE Production(
product_id INT PRIMARY KEY IDENTITY(1,1),
product_name VARCHAR(21),
model_year SMALLINT,
list_price DECIMAL(10,3))

INSERT INTO Production VALUES
('Trek 820', 2016, 379.990),
('Ritchey Timberwolf', 2016, 749.990),
('Surly Wednesday', 2016, 999.990),
('Sun Bicycles Cruz', 2017, 449.990),
('Electra Townie', 2018, 269.990),
('Surly Straggler', 2016, 1549.000),
('Trek Checkpoint', 2019, 2899.990),
('Surly Midnight', 2019, 1250.000);

SELECT * FROM Production

--1. Retrieve product details (product_name, model_year, list_price).
SELECT product_name, model_year, list_price FROM Production

--2. Compare each product’s price with the average price of products in the same category using a nested query.
SELECT product_name,model_year,list_price from Production WHERE list_price <
(SELECT AVG(list_price) from Production)

--3. Display only those products whose price is greater than the category average.
 SELECT product_name,list_price FROM Production WHERE list_price
 > (SELECT AVG(list_price) from Production)

--4. Show calculated difference between product price and category average.
 SELECT list_price-
(SELECT AVG(p2.list_price) FROM Production p2 WHERE p2.model_year = p1.model_year)
AS price_difference FROM Production p1

--5. Concatenate product name and model year as a single column (e.g., 'ProductName (2017)').
SELECT CONCAT (product_name,'(',model_year,')') AS Product_info from Production

