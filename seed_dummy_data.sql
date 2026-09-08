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

-- Seed dummy Reviews for products using existing customer (UserID=3 = John Doe)
INSERT INTO Reviews (CustomerID, ProductID, Rating, Comment) VALUES
(3, 1, 5, 'Absolutely love this case! Crystal clear and fits perfectly.'),
(3, 2, 4, 'Great quality leather, looks premium. Slightly bulky though.'),
(3, 4, 5, 'Super sturdy, my phone survived a 5-foot drop!'),
(3, 5, 3, 'Good case for the price, does the job.'),
(3, 7, 5, 'Looks and feels amazing, highly recommend.');
GO

-- Seed dummy Offers (coupons) linked to the Shop (ShopID = 1)
INSERT INTO Offers (ShopID, OfferName, DiscountValue, Status) VALUES
(1, 'DISCOUNT10', 10.00, 'Active'),
(1, 'HALFPRICE', 50.00, 'Active'),
(1, 'SAMSUNG20', 20.00, 'Active');
GO

-- Seed dummy Orders for customer John Doe (UserID = 3)
INSERT INTO Orders (CustomerID, OrderDate, TotalAmount, PaymentMethod) VALUES
(3, DATEADD(day, -30, GETDATE()), 799.00, 'Card'),
(3, DATEADD(day, -15, GETDATE()), 1648.00, 'Cash on Delivery'),
(3, DATEADD(day, -5, GETDATE()), 499.00, 'Card');
GO
