-- Create the database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'SmartphoneCoverShopDB')
    CREATE DATABASE [SmartphoneCoverShopDB];
GO
USE SmartphoneCoverShopDB;
GO

-- Drop existing tables (reverse order to satisfy foreign keys)
IF OBJECT_ID('Offers', 'U') IS NOT NULL DROP TABLE Offers;
GO
IF OBJECT_ID('Reviews', 'U') IS NOT NULL DROP TABLE Reviews;
GO
IF OBJECT_ID('Payments', 'U') IS NOT NULL DROP TABLE Payments;
GO
IF OBJECT_ID('OrderItems', 'U') IS NOT NULL DROP TABLE OrderItems;
GO
IF OBJECT_ID('Orders', 'U') IS NOT NULL DROP TABLE Orders;
GO
IF OBJECT_ID('Cart', 'U') IS NOT NULL DROP TABLE Cart;
GO
IF OBJECT_ID('Products', 'U') IS NOT NULL DROP TABLE Products;
GO
IF OBJECT_ID('Categories', 'U') IS NOT NULL DROP TABLE Categories;
GO
IF OBJECT_ID('Shops', 'U') IS NOT NULL DROP TABLE Shops;
GO
IF OBJECT_ID('Users', 'U') IS NOT NULL DROP TABLE Users;
GO

-- 4.2.1 Users (Restored Phone)
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    UserType VARCHAR(20) NOT NULL,
    Phone VARCHAR(15) NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    Status INT NOT NULL
);
GO

-- 4.2.2 Shops (Dropped CreatedAt)
CREATE TABLE Shops (
    ShopID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT UNIQUE FOREIGN KEY REFERENCES Users(UserID),
    ShopName VARCHAR(100) NOT NULL,
    ShopDescription TEXT NULL,
    Status INT NOT NULL
);
GO

-- 4.2.3 Categories (Dropped Description, CreatedAt)
CREATE TABLE Categories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName VARCHAR(100) NOT NULL,
    Status INT NOT NULL
);
GO

-- 4.2.4 Products (Dropped ImageURL, CreatedAt, Status)
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ShopID INT NOT NULL FOREIGN KEY REFERENCES Shops(ShopID),
    CategoryID INT NOT NULL FOREIGN KEY REFERENCES Categories(CategoryID),
    ProductName VARCHAR(150) NOT NULL,
    Description TEXT NULL,
    Price DECIMAL(10,2) NOT NULL,
    StockQuantity INT NOT NULL
);
GO

-- 4.2.5 Cart (Dropped AddedDate)
CREATE TABLE Cart (
    CartID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    ProductID INT NOT NULL FOREIGN KEY REFERENCES Products(ProductID),
    Quantity INT NOT NULL
);
GO

-- 4.2.6 Orders (Dropped Status)
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10,2) NOT NULL,
    PaymentMethod VARCHAR(50) NOT NULL
);
GO

-- 4.2.7 OrderItems
CREATE TABLE OrderItems (
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL FOREIGN KEY REFERENCES Orders(OrderID),
    ProductID INT NOT NULL FOREIGN KEY REFERENCES Products(ProductID),
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL
);
GO

-- 4.2.8 Payments
CREATE TABLE Payments (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT UNIQUE NOT NULL FOREIGN KEY REFERENCES Orders(OrderID),
    PaymentMethod VARCHAR(50) NOT NULL,
    TransactionID VARCHAR(100) NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentStatus VARCHAR(20) NOT NULL,
    PaidAt DATETIME DEFAULT GETDATE()
);
GO

-- 4.2.9 Reviews (Dropped CreatedAt)
CREATE TABLE Reviews (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    ProductID INT NOT NULL FOREIGN KEY REFERENCES Products(ProductID),
    Rating TINYINT CHECK (Rating >= 1 AND Rating <= 5),
    Comment TEXT NULL
);
GO

-- 4.2.10 Offers (Dropped CreatedAt, MinOrderAmount, DiscountType. Switched to ShopID)
CREATE TABLE Offers (
    OfferID INT IDENTITY(1,1) PRIMARY KEY,
    ShopID INT NOT NULL FOREIGN KEY REFERENCES Shops(ShopID),
    OfferName VARCHAR(100) NOT NULL,
    DiscountValue DECIMAL(10,2) NOT NULL,
    Status VARCHAR(20) NOT NULL
);
GO

-- Insert seed users (1 Super Admin, 2 Shop Owners, 3 Customers)
INSERT INTO Users (FullName, Email, Password, UserType, Status, Phone)
VALUES 
('Super Administrator', 'superadmin@covershop.com', 'admin123', 'super_admin', 1, '1234567890'),
('Cover Planet Store', 'shopowner@covershop.com', 'owner123', 'admin', 1, '1234567891'),
('Tech Cases Ltd', 'shop2@covershop.com', 'owner123', 'admin', 1, '1234567892'),
('John Doe', 'customer@covershop.com', 'customer123', 'customer', 1, '1234567893'),
('Jane Smith', 'customer2@covershop.com', 'customer123', 'customer', 1, '1234567894'),
('Alice Brown', 'customer3@covershop.com', 'customer123', 'customer', 1, '1234567895');
GO

-- Seed dummy shops for the admins
INSERT INTO Shops (UserID, ShopName, ShopDescription, Status)
VALUES 
(2, 'Cover Planet', 'Best covers in town', 1),
(3, 'Tech Cases', 'Premium tech cases', 1);
GO

-- Seed dummy Categories
INSERT INTO Categories (CategoryName, Status) VALUES
('iPhone Cases', 1),
('Samsung Cases', 1),
('OnePlus Cases', 1),
('Xiaomi Cases', 1),
('Universal Cases', 1);
GO

-- Seed dummy Products (ShopID = 1 = Cover Planet)
INSERT INTO Products (ShopID, CategoryID, ProductName, Description, Price, StockQuantity) VALUES
(1, 1, 'iPhone 15 Pro Clear Case', 'Ultra-thin transparent case with military-grade protection for iPhone 15 Pro.', 799.00, 50),
(1, 1, 'iPhone 14 Leather Wallet Case', 'Genuine leather case with card slots and magnetic closure for iPhone 14.', 1299.00, 30),
(1, 1, 'iPhone 13 Silicone Case', 'Soft-touch silicone case in midnight blue for iPhone 13.', 599.00, 75),
(1, 2, 'Samsung S24 Ultra Rugged Case', 'Heavy-duty drop-proof case with built-in kickstand for Samsung S24 Ultra.', 950.00, 40),
(1, 2, 'Samsung A54 Slim Cover', 'Minimalist slim cover with matte finish for Samsung Galaxy A54.', 399.00, 100),
(1, 2, 'Samsung S23 Mirror Case', 'Stylish flip case with a built-in mirror for Galaxy S23.', 699.00, 25),
(1, 3, 'OnePlus 12 Carbon Fiber Case', 'Carbon fiber texture case with maximum grip for OnePlus 12.', 849.00, 20),
(1, 3, 'OnePlus Nord CE4 Shockproof Case', 'Multi-layer shockproof case for OnePlus Nord CE4.', 499.00, 60),
(1, 4, 'Xiaomi 14 Transparent Case', 'Crystal clear hard case with dust plug for Xiaomi 14.', 349.00, 80),
(1, 5, 'Universal Waterproof Pouch', 'IPX8 waterproof pouch for phones up to 7 inches.', 299.00, 120);
GO

-- Seed dummy Reviews for products using customer John Doe (UserID = 4)
INSERT INTO Reviews (CustomerID, ProductID, Rating, Comment) VALUES
(4, 1, 5, 'Absolutely love this case! Crystal clear and fits perfectly.'),
(4, 2, 4, 'Great quality leather, looks premium. Slightly bulky though.'),
(4, 4, 5, 'Super sturdy, my phone survived a 5-foot drop!'),
(4, 5, 3, 'Good case for the price, does the job.'),
(4, 7, 5, 'Looks and feels amazing, highly recommend.');
GO

-- Seed dummy Offers (coupons) linked to the Shop (ShopID = 1)
INSERT INTO Offers (ShopID, OfferName, DiscountValue, Status) VALUES
(1, 'DISCOUNT10', 10.00, 'Active'),
(1, 'HALFPRICE', 50.00, 'Active'),
(1, 'SAMSUNG20', 20.00, 'Active');
GO

-- Seed dummy Cart items for Customer (UserID = 4)
INSERT INTO Cart (CustomerID, ProductID, Quantity) VALUES
(4, 3, 2),
(4, 6, 1),
(4, 9, 3);
GO

-- Seed dummy Orders for customer John Doe (UserID = 4)
INSERT INTO Orders (CustomerID, OrderDate, TotalAmount, PaymentMethod) VALUES
(4, DATEADD(day, -30, GETDATE()), 799.00, 'Card'),
(4, DATEADD(day, -15, GETDATE()), 1648.00, 'Cash on Delivery'),
(4, DATEADD(day, -5, GETDATE()), 499.00, 'Card');
GO

-- Seed dummy OrderItems
-- Order 1: 1 x iPhone 15 Pro Clear Case (799)
INSERT INTO OrderItems (OrderID, ProductID, Quantity, UnitPrice, Subtotal) VALUES
(1, 1, 1, 799.00, 799.00);

-- Order 2: 2 x Samsung S24 Ultra Case (950 each = 1900, minus discount = 1648)
INSERT INTO OrderItems (OrderID, ProductID, Quantity, UnitPrice, Subtotal) VALUES
(2, 4, 2, 950.00, 1648.00);

-- Order 3: 1 x OnePlus Shockproof Case (499)
INSERT INTO OrderItems (OrderID, ProductID, Quantity, UnitPrice, Subtotal) VALUES
(3, 8, 1, 499.00, 499.00);
GO

-- Seed dummy Payments
INSERT INTO Payments (OrderID, PaymentMethod, TransactionID, Amount, PaymentStatus, PaidAt) VALUES
(1, 'Card', 'TXN-938210', 799.00, 'Completed', DATEADD(day, -30, GETDATE())),
(2, 'Cash on Delivery', NULL, 1648.00, 'Pending', NULL),
(3, 'Card', 'TXN-847291', 499.00, 'Completed', DATEADD(day, -5, GETDATE()));
GO


-- =======================================================
-- REPORTING QUERIES (Joins, Group By, Having, Aggregate)
-- =======================================================

-- 1. View the total revenue generated by each shop that has made at least $1000 in sales
SELECT 
    s.ShopName,
    COUNT(oi.OrderItemID) AS TotalItemsSold,
    SUM(oi.Subtotal) AS TotalRevenue
FROM Shops s
JOIN Products p ON s.ShopID = p.ShopID
JOIN OrderItems oi ON p.ProductID = oi.ProductID
GROUP BY s.ShopName
HAVING SUM(oi.Subtotal) >= 1000;
GO

-- 2. View the average rating of products per category
SELECT 
    c.CategoryName,
    AVG(CAST(r.Rating AS FLOAT)) AS AverageRating,
    COUNT(r.ReviewID) AS TotalReviews
FROM Categories c
JOIN Products p ON c.CategoryID = p.CategoryID
JOIN Reviews r ON p.ProductID = r.ProductID
GROUP BY c.CategoryName
HAVING COUNT(r.ReviewID) > 0;
GO
