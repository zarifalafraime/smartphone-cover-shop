# UI Navigation Diagram

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
