CREATE DATABASE Bookstore
USE Bookstore

/*
Assignment Case Study: Stored Procedures & Transactions in SQL Server 
 Business Scenario – “BookMart Online Bookstore” (Simplified)
BookMart needs a reliable way to place customer orders without overselling books. When a customer orders a book:
•	Check if enough stock is available.
•	If yes → reduce stock and record the order.
•	If no → do not change anything (no partial updates).
Your task is to implement this safely using one stored procedure with transaction control and basic error handling.
Database Schema (Use this – create if needed)
SQL
CREATE TABLE Books (
    BookID  INT IDENTITY(1,1) PRIMARY KEY,
    Title   NVARCHAR(150) NOT NULL,
    Stock   INT NOT NULL CHECK (Stock >= 0),
    Price   DECIMAL(10,2) NOT NULL
);

CREATE TABLE Orders (
    OrderID    INT IDENTITY(1,1) PRIMARY KEY,
    BookID     INT NOT NULL,
    Quantity   INT NOT NULL CHECK (Quantity > 0),
    OrderDate  DATETIME2 DEFAULT SYSDATETIME(),
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);
Assignment Tasks  
Task 1: Stored Procedure to Add a Book  
Create a stored procedure named sp_AddNewBook that takes: @Title NVARCHAR(150), @Stock INT, @Price DECIMAL(10,2)
•	Insert the new book into the Books table.
•	Use TRY…CATCH to handle errors (e.g., invalid stock or price).
•	Print a success message or error message.
Task 2: Main Stored Procedure – Place Order with Transaction  
Create a stored procedure named sp_PlaceOrder with parameters: @BookID INT, @Quantity INT
Must include all of the following:
1.	SET XACT_ABORT ON; at the beginning.
2.	BEGIN TRY
o	BEGIN TRANSACTION
o	Check if book exists and Stock >= @Quantity
	If not → RAISERROR('Not enough stock or book not found.', 16, 1);
o	UPDATE Books SET Stock = Stock - @Quantity WHERE BookID = @BookID;
o	INSERT INTO Orders (BookID, Quantity) VALUES (@BookID, @Quantity);
o	COMMIT TRANSACTION;
o	Print success message: 'Order placed successfully.'
3.	END TRY
4.	BEGIN CATCH
o	If @@TRANCOUNT > 0 then ROLLBACK TRANSACTION;
o	Print error details: number + message (use ERROR_NUMBER(), ERROR_MESSAGE())
o	Example: 'Error ' + CAST(ERROR_NUMBER() AS VARCHAR) + ': ' + ERROR_MESSAGE()
5.	END CATCH
Task 3: Testing & Output  
Insert 3–5 sample books (you can do this manually or using sp_AddNewBook).
Run and show results (screenshots or text output) for at least these three cases:
1.	Successful order → stock decreases, order is inserted.
2.	Insufficient stock → error message, no change in stock or orders table.
3.	Invalid BookID (book does not exist) → error, rollback happens.
 
 
*/


CREATE TABLE Books (
    BookID  INT IDENTITY(1,1) PRIMARY KEY,
    Title   NVARCHAR(150) NOT NULL,
    Stock   INT NOT NULL CHECK (Stock >= 0),
    Price   DECIMAL(10,2) NOT NULL
);

CREATE TABLE Orders (
    OrderID    INT IDENTITY(1,1) PRIMARY KEY,
    BookID     INT NOT NULL,
    Quantity   INT NOT NULL CHECK (Quantity > 0),
    OrderDate  DATETIME2 DEFAULT SYSDATETIME(),
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);

CREATE PROCEDURE sp_AddNewBook 
@Title VARCHAR(150), 
@Stock INT, 
@Price DECIMAL(10,2)
AS 
BEGIN
BEGIN TRY
INSERT INTO Books(Title,Stock,Price) VALUES(@Title,@Stock,@Price);
PRINT 'Success:Book"'+@Title+'"added to inventory.';
END TRY
BEGIN CATCH
PRINT 'Error:'+ ERROR_MESSAGE();
END CATCH
END;

EXEC sp_AddNewBook 'ATOMIC HABITS', 10, 15.99;
EXEC sp_AddNewBook 'RICH DAD POOR DAD', 5, 45.00;
EXEC sp_AddNewBook 'PSYCHOLOGY OF MONEY', 2, 60.00;

CREATE PROCEDURE sp_PlaceOrder
@BookID INT,
@Quantity INT
AS 
BEGIN
SET XACT_ABORT ON; 
BEGIN TRY
Begin TRANSACTION;
IF NOT EXISTS (SELECT 1 FROM Books WHERE BookID=@BookID AND Stock>=@Quantity)
BEGIN
RAISERROR('Not enough stock or book not found.', 16, 1);
END
UPDATE Books SET Stock = Stock - @Quantity WHERE BookID = @BookID;
INSERT INTO Orders (BookID, Quantity) VALUES (@BookID, @Quantity);
COMMIT TRANSACTION;
Print 'Order placed successfully.'
END TRY
BEGIN CATCH
If @@TRANCOUNT > 0
ROLLBACK TRANSACTION;
PRINT 'Error ' + CAST(ERROR_NUMBER() AS VARCHAR) + ': ' + ERROR_MESSAGE()
END CATCH
END;

EXEC sp_PlaceOrder @BookID = 1,@Quantity = 2;
EXEC sp_PlaceOrder @BookID = 3, @Quantity = 10;
EXEC sp_PlaceOrder @BookID = 99, @Quantity = 1;