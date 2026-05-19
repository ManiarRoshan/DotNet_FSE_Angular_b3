CREATE TABLE Customers (
    customer_id INT PRIMARY KEY IDENTITY(1,1),
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);

CREATE TABLE Orders (
    order_id INT PRIMARY KEY IDENTITY(101,1),
    customer_id INT,
    order_value DECIMAL(10,2),
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)
);
INSERT INTO Customers (first_name, last_name) VALUES 
('Amit', 'Sharma'),('Priya', 'Patel'),('Rahul', 'Verma'),('Sita', 'Iyer');    

INSERT INTO Orders (customer_id, order_value) VALUES 
(1, 7000.00), (1, 4000.00),(2, 6000.00),(3, 1500.00)  


--1. Use nested query to calculate total order value per customer.
select customer_id,(SELECT SUM(order_value) FROM Orders o WHERE o.customer_id = c.customer_id) AS Total_order FROM Customers c

--2. Classify customers using conditional logic:
  -- - 'Premium' if total order value > 10000
  -- - 'Regular' if total order value between 5000 and 10000
 --  - 'Basic' if total order value < 5000

 SELECT *,
    CASE 
        WHEN(SELECT SUM(order_value) FROM Orders O WHERE O.customer_id = C.customer_id) > 10000 THEN 'Premium'
        WHEN(SELECT SUM(order_value) FROM Orders O WHERE O.customer_id = C.customer_id)BETWEEN 5000 AND 10000 THEN 'Regular'
        ELSE 'Basic'
        END AS CustomerClass FROM Customers C WHERE EXISTS (SELECT 1 FROM Orders O WHERE O.customer_id=C.customer_id);



--3. Use UNION to display customers with orders and customers without orders.

SELECT first_name + ' ' + last_name AS FullName, 'Active' AS Status FROM Customers 
WHERE customer_id IN(SELECT customer_id FROM Orders)
UNION
SELECT first_name + ' '+last_name AS FullName,'Inactive' AS Status FROM Customers 
WHERE customer_id NOT IN(SELECT customer_id FROM Orders);

--4. Display full name using string concatenation.
SELECT CONCAT(first_name,' ',last_name)AS Full_Name FROM Customers

--5. Handle NULL cases appropriately.
SELECT *,ISNULL((SELECT SUM(order_value) FROM Orders O 
            WHERE O.customer_id = C.customer_id), 0.00) AS FinalTotal FROM Customers C;


