CREATE DATABASE BikeStoreDB;
GO
USE BikeStoreDB;
GO

-- Create Orders Table
CREATE TABLE orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),
    customer_name VARCHAR(100),
    order_status INT, -- 4 = Completed
    order_date DATE DEFAULT GETDATE(),
    shipped_date DATE NULL
);

-- Insert Seed Data
INSERT INTO orders (customer_name, order_status, shipped_date)
VALUES 
('John Doe', 1, NULL),           -- Pending
('Jane Smith', 4, '2023-10-01'), -- Already Completed
('Alice Brown', 2, NULL);        -- Processing
GO

CREATE TRIGGER trg_OrdrStatusValidation
ON orders
AFTER UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE order_status = 4 AND shipped_date IS NULL
    )
    BEGIN
        RAISERROR ('Shipped Date cannot be NULL when order is completed', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- This will FAIL (as intended) because shipped_date is NULL
UPDATE orders
SET order_status = 4
WHERE order_id = 1;

-- Run this to actually complete the order
UPDATE orders
SET order_status = 4, 
    shipped_date = GETDATE() 
WHERE order_id = 1;

-- Then run this to see the change
SELECT * FROM orders;

-- -----------------------------------------------------------

