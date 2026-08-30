# Smartphone Cover Shop

A multi-role Windows Forms desktop application built on **.NET Framework 4.7.2** and **Microsoft SQL Server**, providing dedicated portals for **Super Admin**, **Shop Owner (Admin)**, and **Customer**.

---

## 🗄️ Database Setup

The database script is located at [`Database/SetupDatabase.sql`](file:///C:/Users/Rianul%20Amin%20Rian/Desktop/smartphone-cover-shop/Database/SetupDatabase.sql).

### `Users` Table Schema

| Column | Data Type | Constraint | Description |
| :--- | :--- | :--- | :--- |
| `UserID` | `INT IDENTITY` | PK | Unique user identifier |
| `FullName` | `VARCHAR(100)` | NOT NULL | User's full name |
| `Email` | `VARCHAR(100)` | UNIQUE, NOT NULL | Login email address |
| `Password` | `VARCHAR(255)` | NOT NULL | User password |
| `UserType` | `VARCHAR(20)` | NOT NULL | System role: `super_admin`, `admin`, or `customer` |
| `Phone` | `VARCHAR(15)` | NULL | User contact number |
| `CreatedAt` | `DATETIME` | DEFAULT GETDATE() | Account creation date and time |
| `Status` | `INT` | NOT NULL (1 = active, 0 = inactive) | Account status |

---

## 🔑 Pre-Seeded Test Accounts

| Role | Email | Password | Role Code |
| :--- | :--- | :--- | :--- |
| **Super Admin** | `superadmin@covershop.com` | `admin123` | `super_admin` |
| **Shop Owner** | `shopowner@covershop.com` | `owner123` | `admin` |
| **Customer** | `customer@covershop.com` | `customer123` | `customer` |

---

## 🚀 Features Implemented

1. **Authentication System**:
   - **Login Form (`frmLogin`)**: Clean flat UI, parameterized SQL queries, password toggle, active status check (`Status == 1`), and automatic role-based routing.
   - **Registration Form (`frmRegister`)**: Full name, email, phone number, account type (`Customer` / `Shop Owner (Admin)`), password matching, and validation.
2. **Role-Based Routing to Empty Dashboards**:
   - `super_admin` ➔ [`frmSuperAdminDashboard`](file:///C:/Users/Rianul%20Amin%20Rian/Desktop/smartphone-cover-shop/SmartphoneCoverShop/Forms/frmSuperAdminDashboard.cs)
   - `admin` (Shop Owner) ➔ [`frmAdminDashboard`](file:///C:/Users/Rianul%20Amin%20Rian/Desktop/smartphone-cover-shop/SmartphoneCoverShop/Forms/frmAdminDashboard.cs)
   - `customer` ➔ [`frmCustomerDashboard`](file:///C:/Users/Rianul%20Amin%20Rian/Desktop/smartphone-cover-shop/SmartphoneCoverShop/Forms/frmCustomerDashboard.cs)
3. **Session Management**:
   - User state maintained through [`UserSession`](file:///C:/Users/Rianul%20Amin%20Rian/Desktop/smartphone-cover-shop/SmartphoneCoverShop/Services/UserSession.cs) with full logout support returning to [`frmLogin`](file:///C:/Users/Rianul%20Amin%20Rian/Desktop/smartphone-cover-shop/SmartphoneCoverShop/Forms/frmLogin.cs).

---

## 📂 Project Structure

```
smartphone-cover-shop/
├── Database/
│   └── SetupDatabase.sql
├── SmartphoneCoverShop/
│   ├── App.config
│   ├── Program.cs
│   ├── Data/
│   │   └── DbHelper.cs
│   ├── Models/
│   │   └── User.cs
│   ├── Services/
│   │   ├── AuthService.cs
│   │   └── UserSession.cs
│   ├── Forms/
│   │   ├── frmLogin.cs / frmLogin.Designer.cs
│   │   ├── frmRegister.cs / frmRegister.Designer.cs
│   │   ├── frmSuperAdminDashboard.cs / frmSuperAdminDashboard.Designer.cs
│   │   ├── frmAdminDashboard.cs / frmAdminDashboard.Designer.cs
│   │   └── frmCustomerDashboard.cs / frmCustomerDashboard.Designer.cs
│   └── SmartphoneCoverShop.csproj
└── SmartphoneCoverShop.sln
```