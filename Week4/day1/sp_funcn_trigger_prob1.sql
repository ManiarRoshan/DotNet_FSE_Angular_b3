-- Create a scalar function to calculate total price after discount.

CREATE FUNCTION dbo.fn_CalculateDiscountedTotal(
@Price DECIMAL(10,2),
@Qty INT,
@Discount DECIMAL(5,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN (ISNULL(@Price, 0)*ISNULL(@Qty,0))*(1-ISNULL(@Discount,0));
END;
GO

SELECT dbo.fn_CalculateDiscountedTotal(1200, 2, 0.10) AS Result;


--Create a table-valued function to return top 5 selling products.

CREATE FUNCTION dbo.fn_GetTopSellingProducts()
RETURNS TABLE
AS
RETURN (
    SELECT TOP 5 p.ProductName,SUM(o.Quantity) as TotalSold
    FROM Products p
    JOIN Orders o ON p.ProductID = o.ProductID
    GROUP BY p.ProductName
    ORDER BY TotalSold DESC
);
GO

SELECT * FROM dbo.fn_GetTop5SellingProducts();


-- Create a stored procedure to generate total sales amount per store.

Alter PROCEDURE sp_GenerateTotalSalesPerStore
AS
BEGIN
    SELECT 
        s.StoreName, 
        SUM(dbo.fn_CalculateDiscountedTotal(p.Price,o.Quantity,o.DiscountRate)) AS TotalSalesAmount
    FROM Stores s
    JOIN Orders o ON s.StoreID =o.StoreID
    JOIN Products p ON o.ProductID =p.ProductID
    GROUP BY s.StoreName;
END;
GO

EXEC sp_GenerateTotalSalesPerStore;

-- Create a stored procedure to retrieve orders by date range.

CREATE PROCEDURE sp_GetOrdersByDateRange
    @StartDate DATE = NULL,
    @EndDate DATE = NULL
AS
BEGIN
    SELECT 
        o.OrderID, 
        o.OrderDate, 
        s.StoreName, 
        p.ProductName, 
        o.Quantity,
        dbo.fn_CalculateDiscountedTotal(p.Price,o.Quantity,o.DiscountRate) AS FinalTotal
    FROM Orders o
    JOIN Stores s ON o.StoreID = s.StoreID
    JOIN Products p ON o.ProductID = p.ProductID
    WHERE o.OrderDate BETWEEN ISNULL(@StartDate,'1900-01-01') AND ISNULL(@EndDate,GETDATE())
    ORDER BY o.OrderDate DESC;
END;
GO

EXEC sp_GetOrdersByDateRange @StartDate='2023-10-01',@EndDate='2023-10-31';


