
CREATE DATABASE Products
USE Products;

CREATE TABLE _Products (
ProductId INT PRIMARY KEY IDENTITY(1,1),
ProductName VARCHAR(100),
Category VARCHAR(50),
Price DECIMAL(10,2)
);
GO

-- Insert

CREATE OR ALTER PROCEDURE usp_InsertProduct
@ProductName VARCHAR(100),
@Category VARCHAR(50),
@Price DECIMAL(10,2)
AS
BEGIN
INSERT INTO _Products (ProductName, Category, Price)
VALUES (@ProductName, @Category, @Price);
END
GO 
-- view

CREATE OR ALTER PROCEDURE usp_GetAllProducts
AS
BEGIN
SELECT * FROM _Products;
END
GO 
-- Update

CREATE OR ALTER PROCEDURE usp_UpdateProduct
@ProductId INT,
@ProductName VARCHAR(100),
@Category VARCHAR(50),
@Price DECIMAL(10,2)
AS
BEGIN
UPDATE _Products
SET ProductName = @ProductName,
Category = @Category,
Price = @Price
WHERE ProductId = @ProductId;
END
GO 
-- Delete

CREATE OR ALTER PROCEDURE usp_DeleteProduct
@ProductId INT
AS
BEGIN
DELETE FROM _Products WHERE ProductId = @ProductId;
END
GO 
-- get element by id

CREATE OR ALTER PROCEDURE usp_GetProductById
@ProductId INT
AS
BEGIN
SELECT * FROM _Products WHERE ProductId = @ProductId;
END
GO 
select * from _products;