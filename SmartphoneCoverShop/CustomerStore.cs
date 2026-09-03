using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SmartphoneCoverShop
{
    internal class CustomerProduct
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ShopName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public decimal AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }

    internal class CustomerCartItem
    {
        public CustomerProduct Product { get; set; }
        public int Quantity { get; set; }

        public decimal LineTotal
        {
            get { return Product.Price * Quantity; }
        }
    }

    internal class CustomerReview
    {
        public string FullName { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    internal class CustomerCategory
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public override string ToString()
        {
            return CategoryName;
        }
    }

    // Keeps the customer features independent from the admin code. If a local database is not
    // set up yet, the dashboard deliberately falls back to the sample catalogue below.
    internal static class CustomerStore
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        private static bool databaseChecked;
        private static bool databaseAvailable;
        private static bool useDemoData;
        private static readonly Dictionary<int, List<CustomerCartItem>> DemoCarts = new Dictionary<int, List<CustomerCartItem>>();
        private static readonly List<DemoReview> DemoReviews = new List<DemoReview>
        {
            new DemoReview { ProductID = 101, FullName = "Nadia Rahman", Rating = 5, ReviewText = "Fits my phone perfectly and the buttons are still easy to press.", CreatedAt = DateTime.Today.AddDays(-3) },
            new DemoReview { ProductID = 101, FullName = "Imran Hossain", Rating = 4, ReviewText = "Nice simple case. The colour is exactly what I expected.", CreatedAt = DateTime.Today.AddDays(-7) },
            new DemoReview { ProductID = 102, FullName = "Tania Sultana", Rating = 5, ReviewText = "The clear finish has stayed clean so far. Very happy with it.", CreatedAt = DateTime.Today.AddDays(-2) },
            new DemoReview { ProductID = 103, FullName = "Fahim Ahmed", Rating = 4, ReviewText = "Good grip and useful camera protection.", CreatedAt = DateTime.Today.AddDays(-5) }
        };

        private class DemoReview : CustomerReview
        {
            public int ProductID { get; set; }
        }

        private static List<CustomerProduct> DemoProducts
        {
            get
            {
                return new List<CustomerProduct>
                {
                    new CustomerProduct { ProductID = 101, CategoryID = 1, ProductName = "Midnight Silicone Case", CategoryName = "Silicone", ShopName = "Cover Corner", Description = "A soft, slim silicone cover with a smooth matte finish and raised camera edge.", Price = 890M, StockQuantity = 12, AverageRating = 4.5M, ReviewCount = 2 },
                    new CustomerProduct { ProductID = 102, CategoryID = 2, ProductName = "Crystal Clear Cover", CategoryName = "Transparent", ShopName = "Everyday Covers", Description = "A lightweight clear cover that lets the original phone colour show through.", Price = 650M, StockQuantity = 18, AverageRating = 5M, ReviewCount = 1 },
                    new CustomerProduct { ProductID = 103, CategoryID = 3, ProductName = "Rugged Shield", CategoryName = "Protective", ShopName = "Case Lab", Description = "A firm dual-layer case made for extra grip and everyday drop protection.", Price = 1250M, StockQuantity = 8, AverageRating = 4M, ReviewCount = 1 },
                    new CustomerProduct { ProductID = 104, CategoryID = 1, ProductName = "Sandstone Soft Case", CategoryName = "Silicone", ShopName = "Cover Corner", Description = "A neutral-coloured, easy-to-hold cover with a soft inner lining.", Price = 950M, StockQuantity = 6, AverageRating = 0M, ReviewCount = 0 },
                    new CustomerProduct { ProductID = 105, CategoryID = 4, ProductName = "Magnetic Wallet Cover", CategoryName = "Wallet", ShopName = "Everyday Covers", Description = "A practical folio-style cover with a magnetic close and card slots.", Price = 1450M, StockQuantity = 4, AverageRating = 0M, ReviewCount = 0 },
                    new CustomerProduct { ProductID = 106, CategoryID = 2, ProductName = "Frosted Clear Case", CategoryName = "Transparent", ShopName = "Case Lab", Description = "A semi-transparent case with a frosted back for reduced fingerprints.", Price = 720M, StockQuantity = 15, AverageRating = 0M, ReviewCount = 0 }
                };
            }
        }

        public static List<CustomerCategory> GetCategories()
        {
            if (!useDemoData && EnsureDatabaseSupport())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand("SELECT CategoryID, CategoryName FROM Categories WHERE Status = 1 ORDER BY CategoryName", connection))
                    {
                        connection.Open();
                        using (SqlCommand productCountCommand = new SqlCommand("SELECT COUNT(*) FROM Products WHERE Status = 1 AND StockQuantity > 0", connection))
                        {
                            if (Convert.ToInt32(productCountCommand.ExecuteScalar()) == 0)
                            {
                                useDemoData = true;
                                return GetDemoProducts()
                                    .GroupBy(product => new { product.CategoryID, product.CategoryName })
                                    .Select(group => new CustomerCategory { CategoryID = group.Key.CategoryID, CategoryName = group.Key.CategoryName })
                                    .OrderBy(category => category.CategoryName)
                                    .ToList();
                            }
                        }
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<CustomerCategory> categories = new List<CustomerCategory>();
                            while (reader.Read())
                            {
                                categories.Add(new CustomerCategory
                                {
                                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                                    CategoryName = reader["CategoryName"].ToString()
                                });
                            }
                            if (categories.Count > 0) return categories;
                        }
                    }
                }
                catch
                {
                    databaseAvailable = false;
                }
            }

            return GetDemoProducts()
                .GroupBy(product => new { product.CategoryID, product.CategoryName })
                .Select(group => new CustomerCategory { CategoryID = group.Key.CategoryID, CategoryName = group.Key.CategoryName })
                .OrderBy(category => category.CategoryName)
                .ToList();
        }

        public static List<CustomerProduct> GetProducts(string search, int categoryId, string sort)
        {
            if (!useDemoData && EnsureDatabaseSupport())
            {
                try
                {
                    List<CustomerProduct> products = GetDatabaseProducts(search, categoryId, sort);
                    if (products.Count > 0)
                    {
                        useDemoData = false;
                        return products;
                    }
                    useDemoData = true;
                }
                catch
                {
                    databaseAvailable = false;
                    useDemoData = true;
                }
            }

            IEnumerable<CustomerProduct> productsFromDemo = GetDemoProducts();
            if (!string.IsNullOrWhiteSpace(search))
            {
                productsFromDemo = productsFromDemo.Where(product =>
                    product.ProductName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    product.Description.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    product.CategoryName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            if (categoryId > 0) productsFromDemo = productsFromDemo.Where(product => product.CategoryID == categoryId);

            return SortProducts(productsFromDemo, sort).ToList();
        }

        public static List<CustomerReview> GetReviews(int productId)
        {
            if (!useDemoData && EnsureDatabaseSupport())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand(@"SELECT ISNULL(u.FullName, 'Customer') AS FullName, r.Rating, r.ReviewText, r.CreatedAt
                                                                    FROM ProductReviews r
                                                                    LEFT JOIN Users u ON r.UserID = u.UserID
                                                                    WHERE r.ProductID = @ProductID
                                                                    ORDER BY r.CreatedAt DESC", connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productId);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<CustomerReview> reviews = new List<CustomerReview>();
                            while (reader.Read())
                            {
                                reviews.Add(new CustomerReview
                                {
                                    FullName = reader["FullName"].ToString(),
                                    Rating = Convert.ToInt32(reader["Rating"]),
                                    ReviewText = reader["ReviewText"].ToString(),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                                });
                            }
                            return reviews;
                        }
                    }
                }
                catch
                {
                    databaseAvailable = false;
                }
            }

            return DemoReviews.Where(review => review.ProductID == productId)
                .OrderByDescending(review => review.CreatedAt)
                .Cast<CustomerReview>()
                .ToList();
        }

        public static List<CustomerCartItem> GetCart(int userId)
        {
            if (!useDemoData && EnsureDatabaseSupport())
            {
                try
                {
                    const string query = @"SELECT p.ProductID, p.CategoryID, p.ProductName, ISNULL(ca.CategoryName, 'Uncategorised') AS CategoryName,
                                                  ISNULL(s.ShopName, 'Independent shop') AS ShopName, ISNULL(p.Description, '') AS Description,
                                                  p.Price, p.StockQuantity, CAST(0 AS DECIMAL(10,2)) AS AverageRating,
                                                  CAST(0 AS INT) AS ReviewCount, sc.Quantity
                                           FROM ShoppingCart sc
                                           INNER JOIN Products p ON sc.ProductID = p.ProductID
                                           LEFT JOIN Categories ca ON p.CategoryID = ca.CategoryID
                                           LEFT JOIN Shops s ON p.ShopID = s.ShopID
                                           WHERE sc.UserID = @UserID AND p.Status = 1
                                           ORDER BY sc.UpdatedAt DESC";
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userId);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<CustomerCartItem> items = new List<CustomerCartItem>();
                            while (reader.Read())
                            {
                                items.Add(new CustomerCartItem
                                {
                                    Product = MapProduct(reader),
                                    Quantity = Convert.ToInt32(reader["Quantity"])
                                });
                            }
                            return items;
                        }
                    }
                }
                catch
                {
                    databaseAvailable = false;
                }
            }

            return GetDemoCart(userId)
                .Select(item => new CustomerCartItem { Product = item.Product, Quantity = item.Quantity })
                .ToList();
        }

        public static bool AddToCart(int userId, CustomerProduct product)
        {
            if (product == null || product.StockQuantity < 1) return false;

            if (!useDemoData && EnsureDatabaseSupport())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        int currentQuantity = 0;
                        using (SqlCommand findCommand = new SqlCommand("SELECT Quantity FROM ShoppingCart WHERE UserID = @UserID AND ProductID = @ProductID", connection))
                        {
                            findCommand.Parameters.AddWithValue("@UserID", userId);
                            findCommand.Parameters.AddWithValue("@ProductID", product.ProductID);
                            object result = findCommand.ExecuteScalar();
                            if (result != null) currentQuantity = Convert.ToInt32(result);
                        }

                        int newQuantity = Math.Min(currentQuantity + 1, product.StockQuantity);
                        if (currentQuantity == 0)
                        {
                            using (SqlCommand insertCommand = new SqlCommand("INSERT INTO ShoppingCart (UserID, ProductID, Quantity, CreatedAt, UpdatedAt) VALUES (@UserID, @ProductID, @Quantity, GETDATE(), GETDATE())", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@UserID", userId);
                                insertCommand.Parameters.AddWithValue("@ProductID", product.ProductID);
                                insertCommand.Parameters.AddWithValue("@Quantity", newQuantity);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            using (SqlCommand updateCommand = new SqlCommand("UPDATE ShoppingCart SET Quantity = @Quantity, UpdatedAt = GETDATE() WHERE UserID = @UserID AND ProductID = @ProductID", connection))
                            {
                                updateCommand.Parameters.AddWithValue("@UserID", userId);
                                updateCommand.Parameters.AddWithValue("@ProductID", product.ProductID);
                                updateCommand.Parameters.AddWithValue("@Quantity", newQuantity);
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        return true;
                    }
                }
                catch
                {
                    databaseAvailable = false;
                }
            }

            List<CustomerCartItem> demoCart = GetDemoCart(userId);
            CustomerCartItem existingItem = demoCart.FirstOrDefault(item => item.Product.ProductID == product.ProductID);
            if (existingItem == null)
            {
                demoCart.Add(new CustomerCartItem { Product = product, Quantity = 1 });
            }
            else if (existingItem.Quantity < product.StockQuantity)
            {
                existingItem.Quantity++;
            }
            return true;
        }

        public static bool UpdateCartQuantity(int userId, CustomerProduct product, int quantity)
        {
            if (product == null) return false;
            if (quantity <= 0) return RemoveFromCart(userId, product.ProductID);
            quantity = Math.Min(quantity, product.StockQuantity);

            if (!useDemoData && EnsureDatabaseSupport())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand("UPDATE ShoppingCart SET Quantity = @Quantity, UpdatedAt = GETDATE() WHERE UserID = @UserID AND ProductID = @ProductID", connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userId);
                        command.Parameters.AddWithValue("@ProductID", product.ProductID);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch
                {
                    databaseAvailable = false;
                }
            }

            CustomerCartItem existingItem = GetDemoCart(userId).FirstOrDefault(item => item.Product.ProductID == product.ProductID);
            if (existingItem != null) existingItem.Quantity = quantity;
            return existingItem != null;
        }

        public static bool RemoveFromCart(int userId, int productId)
        {
            if (!useDemoData && EnsureDatabaseSupport())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand("DELETE FROM ShoppingCart WHERE UserID = @UserID AND ProductID = @ProductID", connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userId);
                        command.Parameters.AddWithValue("@ProductID", productId);
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch
                {
                    databaseAvailable = false;
                }
            }

            List<CustomerCartItem> demoCart = GetDemoCart(userId);
            CustomerCartItem existingItem = demoCart.FirstOrDefault(item => item.Product.ProductID == productId);
            if (existingItem != null) demoCart.Remove(existingItem);
            return existingItem != null;
        }

        public static bool AddReview(int productId, int userId, string fullName, int rating, string reviewText)
        {
            if (rating < 1 || rating > 5 || string.IsNullOrWhiteSpace(reviewText)) return false;

            if (!useDemoData && EnsureDatabaseSupport())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    using (SqlCommand command = new SqlCommand("INSERT INTO ProductReviews (ProductID, UserID, Rating, ReviewText, CreatedAt) VALUES (@ProductID, @UserID, @Rating, @ReviewText, GETDATE())", connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productId);
                        command.Parameters.AddWithValue("@UserID", userId);
                        command.Parameters.AddWithValue("@Rating", rating);
                        command.Parameters.AddWithValue("@ReviewText", reviewText.Trim());
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch
                {
                    databaseAvailable = false;
                }
            }

            DemoReviews.Add(new DemoReview
            {
                ProductID = productId,
                FullName = string.IsNullOrWhiteSpace(fullName) ? "Customer" : fullName,
                Rating = rating,
                ReviewText = reviewText.Trim(),
                CreatedAt = DateTime.Now
            });
            return true;
        }

        private static bool EnsureDatabaseSupport()
        {
            if (databaseChecked) return databaseAvailable;
            databaseChecked = true;
            try
            {
                const string setupSql = @"
                    IF OBJECT_ID(N'dbo.ShoppingCart', N'U') IS NULL
                    BEGIN
                        CREATE TABLE dbo.ShoppingCart
                        (
                            CartID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                            UserID INT NOT NULL,
                            ProductID INT NOT NULL,
                            Quantity INT NOT NULL,
                            CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
                            UpdatedAt DATETIME NOT NULL DEFAULT GETDATE(),
                            CONSTRAINT UQ_ShoppingCart_User_Product UNIQUE (UserID, ProductID)
                        )
                    END
                    IF OBJECT_ID(N'dbo.ProductReviews', N'U') IS NULL
                    BEGIN
                        CREATE TABLE dbo.ProductReviews
                        (
                            ReviewID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                            ProductID INT NOT NULL,
                            UserID INT NOT NULL,
                            Rating INT NOT NULL CHECK (Rating BETWEEN 1 AND 5),
                            ReviewText NVARCHAR(600) NOT NULL,
                            CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
                        )
                    END";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(setupSql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    databaseAvailable = true;
                }
            }
            catch
            {
                databaseAvailable = false;
            }
            return databaseAvailable;
        }

        private static List<CustomerProduct> GetDatabaseProducts(string search, int categoryId, string sort)
        {
            string orderBy = "p.ProductName ASC";
            if (sort == "Price: low to high") orderBy = "p.Price ASC";
            if (sort == "Price: high to low") orderBy = "p.Price DESC";
            if (sort == "Best rated") orderBy = "AverageRating DESC, ReviewCount DESC, p.ProductName ASC";

            string query = @"SELECT p.ProductID, p.CategoryID, p.ProductName, ISNULL(c.CategoryName, 'Uncategorised') AS CategoryName,
                                    ISNULL(s.ShopName, 'Independent shop') AS ShopName, ISNULL(p.Description, '') AS Description,
                                    p.Price, p.StockQuantity,
                                    CAST(ISNULL(AVG(CAST(r.Rating AS DECIMAL(10,2))), 0) AS DECIMAL(10,2)) AS AverageRating,
                                    COUNT(r.ReviewID) AS ReviewCount
                             FROM Products p
                             LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                             LEFT JOIN Shops s ON p.ShopID = s.ShopID
                             LEFT JOIN ProductReviews r ON p.ProductID = r.ProductID
                             WHERE p.Status = 1 AND p.StockQuantity > 0";

            if (!string.IsNullOrWhiteSpace(search)) query += " AND (p.ProductName LIKE @Search OR p.Description LIKE @Search)";
            if (categoryId > 0) query += " AND p.CategoryID = @CategoryID";
            query += @" GROUP BY p.ProductID, p.CategoryID, p.ProductName, c.CategoryName, s.ShopName, p.Description, p.Price, p.StockQuantity
                         ORDER BY " + orderBy;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (!string.IsNullOrWhiteSpace(search)) command.Parameters.AddWithValue("@Search", "%" + search.Trim() + "%");
                if (categoryId > 0) command.Parameters.AddWithValue("@CategoryID", categoryId);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<CustomerProduct> products = new List<CustomerProduct>();
                    while (reader.Read()) products.Add(MapProduct(reader));
                    return products;
                }
            }
        }

        private static CustomerProduct MapProduct(SqlDataReader reader)
        {
            return new CustomerProduct
            {
                ProductID = Convert.ToInt32(reader["ProductID"]),
                CategoryID = Convert.ToInt32(reader["CategoryID"]),
                ProductName = reader["ProductName"].ToString(),
                CategoryName = reader["CategoryName"].ToString(),
                ShopName = reader["ShopName"].ToString(),
                Description = reader["Description"].ToString(),
                Price = Convert.ToDecimal(reader["Price"]),
                StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                AverageRating = Convert.ToDecimal(reader["AverageRating"]),
                ReviewCount = Convert.ToInt32(reader["ReviewCount"])
            };
        }

        private static List<CustomerCartItem> GetDemoCart(int userId)
        {
            if (!DemoCarts.ContainsKey(userId)) DemoCarts.Add(userId, new List<CustomerCartItem>());
            return DemoCarts[userId];
        }

        private static List<CustomerProduct> GetDemoProducts()
        {
            List<CustomerProduct> products = DemoProducts;
            foreach (CustomerProduct product in products)
            {
                List<DemoReview> reviews = DemoReviews.Where(review => review.ProductID == product.ProductID).ToList();
                product.ReviewCount = reviews.Count;
                product.AverageRating = reviews.Count == 0 ? 0M : Convert.ToDecimal(reviews.Average(review => review.Rating));
            }
            return products;
        }

        private static IEnumerable<CustomerProduct> SortProducts(IEnumerable<CustomerProduct> products, string sort)
        {
            if (sort == "Price: low to high") return products.OrderBy(product => product.Price);
            if (sort == "Price: high to low") return products.OrderByDescending(product => product.Price);
            if (sort == "Best rated") return products.OrderByDescending(product => product.AverageRating).ThenByDescending(product => product.ReviewCount);
            return products.OrderBy(product => product.ProductName);
        }
    }
}
