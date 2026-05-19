
CREATE TABLE Store_Products (
    prod_id INT PRIMARY KEY,
    prod_name VARCHAR(100) NOT NULL
);

CREATE TABLE Product_Inventory (
    prod_id INT PRIMARY KEY,
    stock_qty INT NOT NULL CHECK (stock_qty>=0),
    FOREIGN KEY (prod_id) REFERENCES Store_Products(prod_id)
);

CREATE TABLE Customer_Orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),
    prod_id INT,
    order_qty INT NOT NULL,
    FOREIGN KEY (prod_id) REFERENCES Store_Products(prod_id)
);

INSERT INTO Store_Products (prod_id, prod_name) 
VALUES (501,'Gaming Mouse'),
(502,'Mechanical Keyboard');

INSERT INTO Product_Inventory(prod_id, stock_qty) 
VALUES (501, 20),
(502, 5); 

ALTER TRIGGER trg_AutoStockUpdate
ON Customer_Orders
AFTER INSERT
AS
BEGIN;
    DECLARE @Pid INT,@QtyOrdered INT,@InStock INT;
    SELECT @Pid=prod_id, @QtyOrdered = order_qty FROM inserted;

    BEGIN TRY
        SELECT @InStock=stock_qty FROM Product_Inventory WHERE prod_id = @Pid
        IF @InStock<@QtyOrdered
        BEGIN
            RAISERROR('Order Cancelled: Insufficient stock. Available: %d, Requested: %d', 16, 1, @InStock, @QtyOrdered);
            RETURN;
        END

        UPDATE Product_Inventory
        SET stock_qty = stock_qty - @QtyOrdered
        WHERE prod_id = @Pid;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;

