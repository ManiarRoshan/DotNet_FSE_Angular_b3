-- ShopEZ default admin user (run once against ECommerceDb)
-- Login: admin@shopez.com / admin123

USE ECommerceDb;
GO

IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'admin@shopez.com')
BEGIN
    INSERT INTO Users (Name, Email, Password, Role)
    VALUES ('Admin', 'admin@shopez.com', 'admin123', 'Admin');
END
GO
