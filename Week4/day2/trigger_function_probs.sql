create database day_17_HandsOn

/*
Level-2 Problem 1: Transactions and Trigger Implementation
Scenario
Auto retail company wants to ensure stock consistency while placing orders. Whenever an order is placed, stock should reduce automatically and transaction should rollback if stock is insufficient.

📌 Requirements 
- Write a transaction to insert data into orders and order_items tables.
- Check stock availability before confirming order.
- Create a trigger to reduce stock quantity after order insertion.
- Rollback transaction if stock quantity is insufficient.

🛠️ Technical Constraints 
- Use explicit transactions (BEGIN TRANSACTION, COMMIT, ROLLBACK).
- Trigger must handle multiple rows.
- Do not allow negative stock values.
- Maintain referential integrity.
Expectations
- Successful implementation of ACID properties.
- Automatic stock update using trigger.
- Proper rollback mechanism in failure scenarios.

🎯 Learning Outcome 
- Understand transaction management.
- Learn trigger-based automation.
- Implement real-world stock control logic.

*/

CREATE TABLE Products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    stock_quantity INT
);

CREATE TABLE Orders (
    order_id INT PRIMARY KEY,
    order_date DATE,
    order_status INT
);

CREATE TABLE Order_Items (
    order_item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    FOREIGN KEY (order_id) REFERENCES Orders(order_id),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);

INSERT INTO Products VALUES
(1,'Car Battery',10),
(2,'Brake Pad',20),
(3,'Oil Filter',15),
(4,'Head Light',12),
(5,'Engine Oil',25);


INSERT INTO Orders VALUES
(101,'2024-01-10',1),
(102,'2024-01-12',1),
(103,'2024-01-15',2),
(104,'2024-01-18',1),
(105,'2024-01-20',2);

INSERT INTO Order_Items VALUES
(1,101,1,5),
(2,101,3,10),
(3,102,2,15),
(4,102,4,5),
(5,103,5,20);


------------------- Query 1


ALTER TABLE Products
ADD stock_quantity INT;

UPDATE Products SET stock_quantity = 20 WHERE product_id = 1;
UPDATE Products SET stock_quantity = 15 WHERE product_id = 2;


-- 1. Create the Trigger to handle stock reduction and validation
CREATE TRIGGER trg_UpdateStock
ON Order_Items
AFTER INSERT
AS
BEGIN
    -- Check for insufficient stock across all inserted rows
    IF EXISTS (
        SELECT 1
        FROM Products p
        JOIN inserted i ON p.product_id = i.product_id
        WHERE p.stock_quantity < i.quantity
    )
    BEGIN
        RAISERROR ('Insufficient stock for one or more products. Transaction aborted.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Reduce stock quantity if check passes
    UPDATE p
    SET p.stock_quantity = p.stock_quantity - i.quantity
    FROM Products p
    JOIN inserted i ON p.product_id = i.product_id;
END;
GO

-- 2. Transaction to place an order
BEGIN TRY
    BEGIN TRANSACTION;

    INSERT INTO Orders (order_id, order_date, order_status)
    VALUES (106, GETDATE(), 1);

    -- This will fire the trigger
    INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity)
    VALUES (6, 106, 1, 2), (7, 106, 2, 3); 

    COMMIT TRANSACTION;
    PRINT 'Order 106 placed successfully and stock updated.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    PRINT 'Order 106 failed: ' + ERROR_MESSAGE();
END CATCH;



------Query 2

/*
Level-2 Problem 2: Atomic Order Cancellation with SAVEPOINT
Scenario
When cancelling an order, system must restore stock quantities and update order_status to Rejected (3). All actions must be atomic.
📌 Requirements 
- Begin a transaction when cancelling an order.
- Restore stock quantities based on order_items.
- Update order_status to 3.
- Use SAVEPOINT before stock restoration.
- If stock restoration fails, rollback to SAVEPOINT.
- Commit transaction only if all operations succeed.

🛠️ Technical Constraints 
- Use BEGIN TRANSACTION.
- Use SAVE TRANSACTION (SAVEPOINT).
- Use TRY…CATCH with custom error handling.
- Use COMMIT and ROLLBACK appropriately.

Expectations
- Proper use of SAVEPOINT.
- Atomic and consistent transaction handling.
- Accurate stock restoration.
- Robust error management.
🎯 Learning Outcome 
- Understand atomic transactions.
- Use SAVEPOINT effectively.
- Maintain data consistency.
- Implement advanced transaction control.

*/

Alter TRIGGER trg_UpdateStock
ON Order_Items
AFTER INSERT
AS
BEGIN
    -- Check stock BEFORE updating
    IF EXISTS (
        SELECT 1
        FROM Products p
        JOIN inserted i ON p.product_id = i.product_id
        WHERE p.stock_quantity < i.quantity
    )
    BEGIN
        RAISERROR ('Insufficient stock. Transaction rolled back.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Update stock
    UPDATE p
    SET p.stock_quantity = p.stock_quantity - i.quantity
    FROM Products p
    JOIN inserted i ON p.product_id = i.product_id;
END;

BEGIN TRY
    BEGIN TRANSACTION;

    -- Use 108 to avoid duplicate key error
    INSERT INTO Orders (order_id, order_date, order_status)
    VALUES (108, GETDATE(), 1);

    INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity)
    VALUES (9, 108, 1, 2); -- Ordering 2 Car Batteries (ID 1)

    COMMIT TRANSACTION;
    PRINT 'Order 108 placed successfully.';
END TRY
BEGIN CATCH
    -- Only rollback if the trigger hasn't already done it
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    PRINT 'Error: ' + ERROR_MESSAGE();
END CATCH;

SELECT * FROM Products;    -- Stock for Product 1 should be reduced by 2
SELECT * FROM Orders;      -- Order 108 should appear here
SELECT * FROM Order_Items; -- Item 9 should appear here




