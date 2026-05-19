-- Create Database
CREATE DATABASE RevenueDB;
GO
USE RevenueDB;
GO

-- Create Tables
CREATE TABLE Stores (
    StoreID INT PRIMARY KEY,
    StoreName NVARCHAR(100)
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    StoreID INT FOREIGN KEY REFERENCES Stores(StoreID),
    OrderStatus INT -- 4 = Completed
);

CREATE TABLE OrderItems (
    OrderItemID INT PRIMARY KEY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
    Quantity INT,
    ListPrice DECIMAL(10, 2),
    Discount DECIMAL(5, 2) -- e.g., 0.10 for 10%
);

-- Insert Dummy Data
INSERT INTO Stores VALUES (1, 'Downtown Tech'), (2, 'Suburban Gadgets');
INSERT INTO Orders VALUES (101, 1, 4), (102, 1, 4), (103, 1, 1), (104, 2, 4);
INSERT INTO OrderItems VALUES 
(1, 101, 2, 500.00, 0.10),
(2, 102, 1, 200.00, 0.00),
(3, 103, 5, 100.00, 0.50),
(4, 104, 3, 300.00, 0.20);

Drop database RevenueDB

CREATE DATABASE StoreDB;
GO
USE StoreDB;

CREATE TABLE Stores (StoreID INT PRIMARY KEY, StoreName VARCHAR(50));
CREATE TABLE Products (ProductID INT PRIMARY KEY, Price DECIMAL(10,2));
CREATE TABLE Orders (OrderID INT PRIMARY KEY, StoreID INT, DiscountPercent DECIMAL(5,2), OrderStatus INT);
CREATE TABLE OrderDetails (OrderID INT, ProductID INT, Quantity INT);

-- Sample Data
INSERT INTO Stores VALUES (1, 'Apple Store'), (2, 'Samsung Shop');
INSERT INTO Products VALUES (10, 1000.00), (20, 500.00);
INSERT INTO Orders VALUES (101, 1, 10.0, 4), (102, 1, 0.0, 4), (103, 2, 5.0, 4);
INSERT INTO OrderDetails VALUES (101, 10, 1), (102, 20, 2), (103, 10, 2);
GO

CREATE PROCEDURE CalculateRevenue
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @OrderID INT, @StoreID INT, @Discount DECIMAL(5,2), @Revenue DECIMAL(10,2);

    -- Requirement: Store results in a temporary table
    IF OBJECT_ID('tempdb..#TempRevenue') IS NOT NULL DROP TABLE #TempRevenue;
    CREATE TABLE #TempRevenue (StoreID INT, Revenue DECIMAL(10,2));

    -- Cursor for completed orders (Status = 4)
    DECLARE order_cursor CURSOR FOR 
    SELECT OrderID, StoreID, DiscountPercent FROM Orders WHERE OrderStatus = 4;

    BEGIN TRY
        BEGIN TRANSACTION;
        OPEN order_cursor;
        FETCH NEXT FROM order_cursor INTO @OrderID, @StoreID, @Discount;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            -- Calculate Raw Revenue
            SELECT @Revenue = SUM(p.Price * od.Quantity)
            FROM OrderDetails od 
            JOIN Products p ON od.ProductID = p.ProductID
            WHERE od.OrderID = @OrderID;

            -- Apply Discount
            SET @Revenue = ISNULL(@Revenue, 0) - (ISNULL(@Revenue, 0) * ISNULL(@Discount, 0) / 100);

            -- Store in Temp Table
            INSERT INTO #TempRevenue VALUES (@StoreID, @Revenue);

            FETCH NEXT FROM order_cursor INTO @OrderID, @StoreID, @Discount;
        END

        CLOSE order_cursor;
        DEALLOCATE order_cursor;

        -- Display Store-wise Summary
        SELECT StoreID, SUM(Revenue) AS TotalRevenue FROM #TempRevenue GROUP BY StoreID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        IF CURSOR_STATUS('global','order_cursor') >= 0 BEGIN CLOSE order_cursor; DEALLOCATE order_cursor; END
        PRINT 'Error occurred: ' + ERROR_MESSAGE();
    END CATCH
END;
GO

EXEC CalculateRevenue;
