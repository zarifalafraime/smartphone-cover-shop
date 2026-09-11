# Smartphone Cover Shop

A multi-role Windows Forms desktop application built on **.NET Framework 4.7.2** and **Microsoft SQL Server**, providing dedicated portals for **Super Admin**, **Shop Owner (Admin)**, and **Customer**.

---

## Project Documentation

* 📄 **[Download / View the Final Project Report (PDF)](docs/Project_Report.pdf)**

---

## Team Members

| Serial | Name | Student ID | Contribution % | Contribution Details |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Nahiyan | 24-58497-2 | 40% | Customer Dashboard, Shopping & Browsing, Cart & Checkout, Order Tracking, Documentation & Diagrams, Database Finalization |
| 2 | Zarif | 21-44568-1 | 30% | Admin Dashboard, Product Management, Offers & Promotions, Shop Profile, Initial Setup |
| 3 | Mashruf | 22-46073-1 | 30% | Super Admin Dashboard, User & Shop Management, Content Moderation, Database Architecture |

---

## Diagrams

* 🗄️ **[View Full SQL Schema Diagram](docs/diagrams/SQL_Schema_Diagram.md)**
* 🗺️ **[View UI Navigation Diagram](docs/diagrams/UI_Navigation_Diagram.md)**

### SQL Schema Diagram

```mermaid
erDiagram
    Users ||--o{ Shops : "owns"
    Users ||--o{ Cart : "adds to"
    Users ||--o{ Orders : "places"
    Users ||--o{ Reviews : "writes"
    Shops ||--o{ Products : "sells"
    Shops ||--o{ Offers : "creates"
    Categories ||--o{ Products : "categorizes"
    Products ||--o{ Cart : "in"
    Products ||--o{ OrderItems : "in"
    Products ||--o{ Reviews : "has"
    Orders ||--|{ OrderItems : "contains"
    Orders ||--o| Payments : "has"

    Users {
        int UserID PK
        string FullName
        string Email
        string Password
        string UserType
        string Phone
        datetime CreatedAt
        int Status
    }
    Shops {
        int ShopID PK
        int UserID FK
        string ShopName
        string ShopDescription
        int Status
    }
    Categories {
        int CategoryID PK
        string CategoryName
        int Status
    }
    Products {
        int ProductID PK
        int ShopID FK
        int CategoryID FK
        string ProductName
        string Description
        decimal Price
        int StockQuantity
    }
    Cart {
        int CartID PK
        int CustomerID FK
        int ProductID FK
        int Quantity
    }
    Orders {
        int OrderID PK
        int CustomerID FK
        datetime OrderDate
        decimal TotalAmount
        string PaymentMethod
    }
    OrderItems {
        int OrderItemID PK
        int OrderID FK
        int ProductID FK
        int Quantity
        decimal UnitPrice
        decimal Subtotal
    }
    Payments {
        int PaymentID PK
        int OrderID FK
        string PaymentMethod
        string TransactionID
        decimal Amount
        string PaymentStatus
        datetime PaidAt
    }
    Reviews {
        int ReviewID PK
        int CustomerID FK
        int ProductID FK
        int Rating
        string Comment
    }
    Offers {
        int OfferID PK
        int ShopID FK
        string OfferName
        decimal DiscountValue
        string Status
    }
```

### UI Navigation Diagram

```mermaid
graph TD
    Login[Login] -->|super_admin| SAD[Super Admin Dashboard]
    Login -->|admin| AD[Shop Owner Dashboard]
    Login -->|customer| CD[Customer Dashboard]
    Login --> Register[Registration]

    SAD -->|Manage| MU[Manage Users]
    SAD -->|Manage| MSO[Manage Shop Owners]
    SAD -->|Manage| MR[Manage Reviews]
    SAD -->|Manage| MC[Manage Categories]

    AD -->|Manage| MP[Manage Products]
    AD -->|Manage| MO[Manage Coupons]
    AD -->|Manage| MSP[Shop Profile]

    CD -->|Shop| BP[Browse Products]
    CD -->|View| OH[Order History]
    BP -->|Click| PD[Product Details]
    BP -->|Add| Cart[Cart]
    PD -->|Add| Cart
    Cart --> Checkout[Checkout]
```

---

## Screenshots

### Core
![Login Screen](docs/screenshots/01-login.png)
![Registration Screen](docs/screenshots/02-register.png)

### Customer Portal
![Customer Dashboard](docs/screenshots/03-customer-dashboard.png)
![Browse Products](docs/screenshots/04-browse-products.png)
![Product Details](docs/screenshots/05-product-details.png)
![Customer Cart](docs/screenshots/06-customer-cart.png)
![Checkout](docs/screenshots/07-checkout.png)
![Order History](docs/screenshots/08-order-history.png)

### Shop Owner (Admin) Portal
![Admin Dashboard](docs/screenshots/09-admin-dashboard.png)
![Manage Products](docs/screenshots/10-manage-products.png)
![Manage Offers](docs/screenshots/11-manage-offers.png)
![Manage Categories](docs/screenshots/12-manage-categories.png)
![Manage Shop Profile](docs/screenshots/13-manage-shop-profile.png)

### Super Admin Portal
![Manage Shop Owners](docs/screenshots/15-manage-shop-owners.png)
![Manage Users](docs/screenshots/16-manage-users.png)
![Manage Reviews](docs/screenshots/17-manage-reviews.png)

---

## Database Setup & Connection String

1. **Database Script:** The complete database schema, dummy data, and testing queries are all located in a single unified script: [`Database/dbo.Table.sql`](Database/dbo.Table.sql). Execute this in SQL Server Management Studio (SSMS) to create and seed the `SmartphoneCoverShopDB` database.
2. **Connection String:** To run the project locally, open `SmartphoneCoverShop\App.config` and change the `Data Source` in the connection string to match your local SQL Server instance name (e.g., `.` or `.\SQLEXPRESS`).

```xml
<connectionStrings>
    <add name="DefaultConnection" 
         connectionString="Data Source=.;Initial Catalog=SmartphoneCoverShopDB;Integrated Security=True" 
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

---

## Pre-Seeded Test Accounts

| Role | Email | Password | Role Code |
| :--- | :--- | :--- | :--- |
| **Super Admin** | `superadmin@covershop.com` | `admin123` | `super_admin` |
| **Shop Owner** | `shopowner@covershop.com` | `owner123` | `admin` |
| **Customer** | `customer@covershop.com` | `customer123` | `customer` |