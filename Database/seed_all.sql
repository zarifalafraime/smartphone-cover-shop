-- =============================================
-- SEED DATA: 5 rows per table
-- =============================================

-- 1. USERS (2 super_admin, 5 shop owners/admin, 5 customers = we'll do 5 relevant)
-- (3 already inserted by schema: superadmin, shopowner, customer)
-- Add 2 more shop owners (pending) and 2 more customers
INSERT INTO Users (FullName, Email, Password, UserType, Phone, Status) VALUES
('Zarif Al Afraime',   'zarif@covershop.com',    'zarif123',    'super_admin', '01700000001', 1),
('Nahiyan Sajid',      'nahiyan@covershop.com',  'nahiyan123',  'customer',    '01700000002', 1),
('Rahim Ahmed',        'rahim@covershop.com',    'rahim123',    'admin',       '01700000003', 1),
('Sofia Begum',        'sofia@covershop.com',    'sofia123',    'admin',       '01700000004', 2),
('Karim Hossain',      'karim@covershop.com',    'karim123',    'customer',    '01700000005', 1);
GO

-- 2. SHOPS (UserID 2 = Cover Planet already seeded)
-- Add 4 more shops for the new admin users (UserID 4 = Rahim)
INSERT INTO Shops (UserID, ShopName, ShopDescription, Status) VALUES
(4, 'Rahim Cases',      'Premium leather covers for all brands',    1),
(1, 'Admin Demo Shop',  'Demo shop for testing purposes',            1),
(3, 'Customer Shop',    'Test shop entry',                           0),
(5, 'Sofia Covers',     'Stylish covers by Sofia (pending approval)',0);
GO

-- 3. CATEGORIES
-- (none seeded yet)
INSERT INTO Categories (CategoryName, Status) VALUES
('Silicone Cases',      1),
('Leather Cases',       1),
('Transparent Cases',   1),
('Rugged Cases',        1),
('Wallet Cases',        1);
GO

-- 4. PRODUCTS (ShopID 1 = Cover Planet, ShopID 2 = Rahim Cases)
INSERT INTO Products (ShopID, CategoryID, ProductName, Description, Price, StockQuantity) VALUES
(1, 1, 'iPhone 15 Silicone Case',        'Soft silicone protective case for iPhone 15',        599.00, 50),
(1, 2, 'Samsung S24 Leather Wallet',     'Genuine leather wallet case for Samsung S24',        899.00, 30),
(2, 3, 'OnePlus 12 Clear Case',          'Crystal clear hard case for OnePlus 12',             350.00, 75),
(2, 4, 'Pixel 8 Rugged Armor',           'Military grade drop protection for Google Pixel 8',  750.00, 20),
(1, 5, 'iPhone 14 Wallet Flip Cover',    'Flip wallet cover with card slots for iPhone 14',    650.00, 40);
GO

-- 5. CART (CustomerID 3 = John Doe, CustomerID 6 = Nahiyan, CustomerID 8 = Karim)
INSERT INTO Cart (CustomerID, ProductID, Quantity) VALUES
(3, 1, 2),
(3, 3, 1),
(6, 2, 1),
(6, 4, 3),
(8, 5, 1);
GO

-- 6. ORDERS
INSERT INTO Orders (CustomerID, OrderDate, TotalAmount, PaymentMethod) VALUES
(3, GETDATE(),                      1198.00, 'Cash on Delivery'),
(6, DATEADD(day,-1, GETDATE()),      899.00, 'Online Payment'),
(8, DATEADD(day,-2, GETDATE()),      650.00, 'Cash on Delivery'),
(3, DATEADD(day,-5, GETDATE()),      350.00, 'Online Payment'),
(6, DATEADD(day,-7, GETDATE()),     1500.00, 'Cash on Delivery');
GO

-- 7. ORDER ITEMS
INSERT INTO OrderItems (OrderID, ProductID, Quantity, UnitPrice, Subtotal) VALUES
(1, 1, 2, 599.00, 1198.00),
(2, 2, 1, 899.00,  899.00),
(3, 5, 1, 650.00,  650.00),
(4, 3, 1, 350.00,  350.00),
(5, 4, 2, 750.00, 1500.00);
GO

-- 8. PAYMENTS
INSERT INTO Payments (OrderID, PaymentMethod, TransactionID, Amount, PaymentStatus) VALUES
(1, 'Cash on Delivery', NULL,          1198.00, 'Completed'),
(2, 'Online Payment',   'TXN-20240901', 899.00, 'Completed'),
(3, 'Cash on Delivery', NULL,           650.00, 'Pending'),
(4, 'Online Payment',   'TXN-20240896', 350.00, 'Completed'),
(5, 'Cash on Delivery', NULL,          1500.00, 'Failed');
GO

-- 9. REVIEWS
INSERT INTO Reviews (CustomerID, ProductID, Rating, Comment) VALUES
(3, 1, 5, 'Perfect fit and great quality silicone material!'),
(6, 2, 4, 'Nice leather feel but stitching could be better.'),
(8, 5, 5, 'Loved the wallet slots, very convenient!'),
(3, 3, 3, 'It is okay, a bit loose on the corners.'),
(6, 4, 5, 'Extremely durable, survived multiple drops!');
GO

-- 10. OFFERS (ShopID 1 = Cover Planet, ShopID 2 = Rahim Cases)
INSERT INTO Offers (ShopID, OfferName, DiscountValue, Status) VALUES
(1, 'Summer Sale',       10.00, 'Active'),
(1, 'New Customer Deal', 15.00, 'Active'),
(2, 'Clearance Offer',   20.00, 'Active'),
(2, 'Bundle Discount',    5.00, 'Inactive'),
(1, 'Flash Sale',        25.00, 'Inactive');
GO

-- Final check
SELECT 'Users'      AS TableName, COUNT(*) AS Rows FROM Users      UNION ALL
SELECT 'Shops',                   COUNT(*)         FROM Shops       UNION ALL
SELECT 'Categories',              COUNT(*)         FROM Categories  UNION ALL
SELECT 'Products',                COUNT(*)         FROM Products    UNION ALL
SELECT 'Cart',                    COUNT(*)         FROM Cart        UNION ALL
SELECT 'Orders',                  COUNT(*)         FROM Orders      UNION ALL
SELECT 'OrderItems',              COUNT(*)         FROM OrderItems  UNION ALL
SELECT 'Payments',                COUNT(*)         FROM Payments    UNION ALL
SELECT 'Reviews',                 COUNT(*)         FROM Reviews     UNION ALL
SELECT 'Offers',                  COUNT(*)         FROM Offers;
GO
