# Smartphone Cover Shop

A multi-role Windows Forms desktop application built on **.NET Framework 4.7.2** and **Microsoft SQL Server**, providing dedicated portals for **Super Admin**, **Shop Owner (Admin)**, and **Customer**.

---

## Project Documentation

* 📄 **[Download / View the Final Project Report (PDF)](Project_Report.pdf)**

---

## Team Members

| Name | Student ID | Contribution |
| :--- | :--- | :--- |
| Nahiyan | 10001 | Customer Dashboard, UI/UX, Cart & Checkout |
| Zarif | 10002 | Admin Dashboard, Product Management, Offers |
| Mashruf | 10003 | Super Admin Dashboard, Database Schema, User Roles |

---

## Diagrams

### SQL Schema Diagram

```mermaid
erDiagram
    Users ||--o{ Orders : "places"
    Users ||--o{ Reviews : "writes"
    Shops ||--o{ Products : "sells"
    Products ||--o{ Reviews : "has"
    Categories ||--o{ Products : "categorizes"
    Shops ||--o{ Offers : "creates"
    Orders ||--|{ OrderItems : "contains"
    Products ||--o{ OrderItems : "in"
    Orders ||--o| Payments : "has"

    Users {
        int UserID PK
        string FullName
        string Email
        string Password
        string UserType
        string Phone
        int Status
    }
    Shops {
        int ShopID PK
        int OwnerID FK
        string ShopName
        int Status
    }
    Products {
        int ProductID PK
        int ShopID FK
        int CategoryID FK
        string ProductName
        decimal Price
        int StockQuantity
    }
    Orders {
        int OrderID PK
        int CustomerID FK
        decimal TotalAmount
        string PaymentMethod
    }
    OrderItems {
        int OrderID FK
        int ProductID FK
        int Quantity
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

    AD -->|Manage| MP[Manage Products]
    AD -->|Manage| MC[Manage Categories]
    AD -->|Manage| MO[Manage Offers]
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

## Database Setup

The complete database schema, dummy data, and testing queries are all located in a single unified script: [`dbo.Table.sql`](dbo.Table.sql).

---

## Pre-Seeded Test Accounts

| Role | Email | Password | Role Code |
| :--- | :--- | :--- | :--- |
| **Super Admin** | `superadmin@covershop.com` | `admin123` | `super_admin` |
| **Shop Owner** | `shopowner@covershop.com` | `owner123` | `admin` |
| **Customer** | `customer@covershop.com` | `customer123` | `customer` |