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

-- Insert seed users
INSERT INTO Users (FullName, Email, Password, UserType, Status)
VALUES 
('Super Administrator', 'superadmin@covershop.com', 'admin123', 'super_admin', 1),
('Cover Planet Store', 'shopowner@covershop.com', 'owner123', 'admin', 1),
('John Doe', 'customer@covershop.com', 'customer123', 'customer', 1);
GO

-- Seed a dummy shop for the admin
INSERT INTO Shops (UserID, ShopName, ShopDescription, Status)
VALUES (2, 'Cover Planet', 'Best covers in town', 1);
GO
