-- Seed dummy Categories
INSERT INTO Categories (CategoryName, Description, Status) VALUES
('iPhone Cases', 'Premium cases for all iPhone models', 1),
('Samsung Cases', 'Stylish cases for Samsung Galaxy series', 1),
('OnePlus Cases', 'Durable cases for OnePlus devices', 1),
('Xiaomi Cases', 'Affordable cases for Xiaomi phones', 1),
('Universal Cases', 'Fits most smartphones', 1);
GO

-- Seed dummy Products (ShopID = 1 = Cover Planet)
INSERT INTO Products (ShopID, CategoryID, ProductName, Description, Price, StockQuantity, Status) VALUES
(1, 1, 'iPhone 15 Pro Clear Case', 'Ultra-thin transparent case with military-grade protection for iPhone 15 Pro.', 799.00, 50, 1),
(1, 1, 'iPhone 14 Leather Wallet Case', 'Genuine leather case with card slots and magnetic closure for iPhone 14.', 1299.00, 30, 1),
(1, 1, 'iPhone 13 Silicone Case', 'Soft-touch silicone case in midnight blue for iPhone 13.', 599.00, 75, 1),
(1, 2, 'Samsung S24 Ultra Rugged Case', 'Heavy-duty drop-proof case with built-in kickstand for Samsung S24 Ultra.', 950.00, 40, 1),
(1, 2, 'Samsung A54 Slim Cover', 'Minimalist slim cover with matte finish for Samsung Galaxy A54.', 399.00, 100, 1),
(1, 2, 'Samsung S23 Mirror Case', 'Stylish flip case with a built-in mirror for Galaxy S23.', 699.00, 25, 1),
(1, 3, 'OnePlus 12 Carbon Fiber Case', 'Carbon fiber texture case with maximum grip for OnePlus 12.', 849.00, 20, 1),
(1, 3, 'OnePlus Nord CE4 Shockproof Case', 'Multi-layer shockproof case for OnePlus Nord CE4.', 499.00, 60, 1),
(1, 4, 'Xiaomi 14 Transparent Case', 'Crystal clear hard case with dust plug for Xiaomi 14.', 349.00, 80, 1),
(1, 5, 'Universal Waterproof Pouch', 'IPX8 waterproof pouch for phones up to 7 inches.', 299.00, 120, 1);
GO

-- Seed dummy Reviews for products using existing customer (UserID=3 = John Doe)
INSERT INTO Reviews (CustomerID, ProductID, Rating, Comment, CreatedAt) VALUES
(3, 1, 5, 'Absolutely love this case! Crystal clear and fits perfectly.', GETDATE()),
(3, 2, 4, 'Great quality leather, looks premium. Slightly bulky though.', GETDATE()),
(3, 4, 5, 'Super sturdy, my phone survived a 5-foot drop!', GETDATE()),
(3, 5, 3, 'Good case for the price, does the job.', GETDATE()),
(3, 7, 5, 'Looks and feels amazing, highly recommend.', GETDATE());
GO

-- Seed dummy Offers (coupons) linked to the Shop (product-level)
INSERT INTO Offers (ProductID, OfferName, DiscountType, DiscountValue, StartDate, EndDate, Status) VALUES
(1, 'IPHONE10', 'Percentage', 10.00, GETDATE(), DATEADD(year,1,GETDATE()), 'Active'),
(2, 'WALLET15', 'Percentage', 15.00, GETDATE(), DATEADD(year,1,GETDATE()), 'Active'),
(4, 'SAMSUNG20', 'Percentage', 20.00, GETDATE(), DATEADD(year,1,GETDATE()), 'Active'),
(7, 'ONEPLUS10', 'Percentage', 10.00, GETDATE(), DATEADD(year,1,GETDATE()), 'Active');
GO

-- Seed dummy Orders for customer John Doe (UserID = 3)
INSERT INTO Orders (CustomerID, OrderDate, TotalAmount, PaymentMethod, Status) VALUES
(3, DATEADD(day, -30, GETDATE()), 799.00, 'Card', 'Completed'),
(3, DATEADD(day, -15, GETDATE()), 1648.00, 'Cash on Delivery', 'Completed'),
(3, DATEADD(day, -5, GETDATE()), 499.00, 'Card', 'Processing');
GO
