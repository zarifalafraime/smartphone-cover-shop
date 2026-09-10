using System;
using System.Data;
using System.Windows.Forms;

namespace SmartphoneCoverShop
{
    public partial class frmAdminDashboard : Form
    {
        public int LoggedInUserId { get; set; }
        public string LoggedInFullName { get; set; }
        public string LoggedInUserType { get; set; }
        private int shopId = 0;

        public frmAdminDashboard()
        {
            InitializeComponent();
        }

        public frmAdminDashboard(int userId, string fullName, string userType)
        {
            InitializeComponent();
            LoggedInUserId = userId;
            LoggedInFullName = fullName;
            LoggedInUserType = userType;
        }

                private void frmDashboard_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LoggedInFullName))
            {
                lblWelcome.Text = "Welcome, " + LoggedInFullName + " (Shop Owner)";
            }
            
            cmbFilterStock.Items.Add("All");
            cmbFilterStock.Items.Add("Out of Stock (0)");
            cmbFilterStock.Items.Add("Low (1-20)");
            cmbFilterStock.Items.Add("Medium (21-50)");
            cmbFilterStock.Items.Add("High (51+)");
            cmbFilterStock.SelectedIndex = 0;
            
            LoadShopData();
        }
        
        private void LoadShopData()
        {
            using (DataAccess da = new DataAccess())
            {
                // 1. Get ShopID for this user
                string shopQuery = "SELECT ShopID FROM Shops WHERE UserID = '" + LoggedInUserId + "'";
                DataTable dtShop = da.ExecuteQueryTable(shopQuery);
                if (dtShop.Rows.Count > 0)
                {
                    shopId = Convert.ToInt32(dtShop.Rows[0]["ShopID"]);
                    
                    LoadMetrics(da);
                    LoadInventory(da);
                    LoadSalesHistory(da);
                    LoadReviews(da);
                }
                else
                {
                    MessageBox.Show("Please complete your Shop Profile first.", "Profile Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        
        private void LoadMetrics(DataAccess da)
        {
            string query = @"
                SELECT 
                    ISNULL(SUM(oi.Quantity), 0) as TotalSold,
                    ISNULL(SUM(oi.Subtotal), 0) as TotalRevenue
                FROM OrderItems oi
                INNER JOIN Products p ON oi.ProductID = p.ProductID
                INNER JOIN Orders o ON oi.OrderID = o.OrderID
                WHERE p.ShopID = " + shopId + "";
                
            DataTable dt = da.ExecuteQueryTable(query);
            if (dt.Rows.Count > 0)
            {
                int totalSold = Convert.ToInt32(dt.Rows[0]["TotalSold"]);
                decimal totalRev = Convert.ToDecimal(dt.Rows[0]["TotalRevenue"]);
                lblTotalSold.Text = "Products Sold: " + totalSold;
                lblTotalRevenue.Text = "Total Revenue: $" + totalRev.ToString("0.00");
            }
        }

        private void LoadInventory(DataAccess da)
        {
            string query = @"
                SELECT 
                    p.ProductID, 
                    p.ProductName, 
                    c.CategoryName, 
                    p.Price, 
                    p.StockQuantity 
                FROM Products p
                LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                WHERE p.ShopID = " + shopId;
            dgvInventory.DataSource = da.ExecuteQueryTable(query);
        }

        private void LoadSalesHistory(DataAccess da)
        {
            string query = @"
                SELECT 
                    o.OrderDate,
                    o.OrderID,
                    p.ProductName,
                    oi.Quantity,
                    oi.Subtotal
                FROM OrderItems oi
                INNER JOIN Products p ON oi.ProductID = p.ProductID
                INNER JOIN Orders o ON oi.OrderID = o.OrderID
                WHERE p.ShopID = " + shopId + @"
                ORDER BY o.OrderDate DESC";
            dgvSalesHistory.DataSource = da.ExecuteQueryTable(query);
        }

        private void LoadReviews(DataAccess da)
        {
            string query = @"
                SELECT 
                    p.ProductName,
                    u.FullName as CustomerName,
                    r.Rating,
                    r.Comment
                FROM Reviews r
                INNER JOIN Products p ON r.ProductID = p.ProductID
                INNER JOIN Users u ON r.CustomerID = u.UserID
                WHERE p.ShopID = " + shopId + @"
                ORDER BY r.ReviewID DESC";
            dgvReviews.DataSource = da.ExecuteQueryTable(query);
        }

                private void cmbFilterStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (shopId != 0)
            {
                using (DataAccess da = new DataAccess())
                {
                    LoadInventory(da);
                }
            }
        }

        private void btnManageProfile_Click(object sender, EventArgs e)
        {
            new frmManageShopProfile(LoggedInUserId).ShowDialog();
            LoadShopData();
        }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            new frmManageProducts(LoggedInUserId).ShowDialog();
            LoadShopData();
        }

        private void btnManageCoupons_Click(object sender, EventArgs e)
        {
            if (shopId == 0)
            {
                MessageBox.Show("Please setup your shop profile first.");
                return;
            }
            new frmManageOffers(shopId).ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Close();
        }
    }
}




