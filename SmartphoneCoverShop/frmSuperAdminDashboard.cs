using System;
using System.Windows.Forms;
using System.Data;

namespace SmartphoneCoverShop
{
    public partial class frmSuperAdminDashboard : Form
    {
        public int LoggedInUserId { get; set; }
        public string LoggedInFullName { get; set; }
        public string LoggedInUserType { get; set; }

        public frmSuperAdminDashboard()
        {
            InitializeComponent();
        }

        public frmSuperAdminDashboard(int userId, string fullName, string userType)
        {
            InitializeComponent();
            LoggedInUserId = userId;
            LoggedInFullName = fullName;
            LoggedInUserType = userType;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Close();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LoggedInFullName))
            {
                string roleDisplay = LoggedInUserType == null ? "" : LoggedInUserType.ToUpper();
                lblWelcome.Text = "👤 " + LoggedInFullName + " [" + roleDisplay + "]";
            }
            LoadDashboardStats();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnManageShopOwners_Click(object sender, EventArgs e)
        {
            frmManageShopOwners manageForm = new frmManageShopOwners();
            manageForm.ShowDialog();
        }

        private void btnManageCategories_Click(object sender, EventArgs e)
        {
            frmManageCategories categoriesForm = new frmManageCategories();
            categoriesForm.ShowDialog();
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            frmManageUsers usersForm = new frmManageUsers();
            usersForm.ShowDialog();
            LoadDashboardStats();
        }

        private void btnManageReviews_Click(object sender, EventArgs e)
        {
            frmManageReviews reviewsForm = new frmManageReviews();
            reviewsForm.ShowDialog();
            LoadDashboardStats();
        }

        private void LoadDashboardStats()
        {
            try
            {
                using (DataAccess da = new DataAccess())
                {
                    // Calculate Total Sales
                    string salesQuery = "SELECT SUM(TotalAmount) FROM Orders";
                    DataTable dtSales = da.ExecuteQueryTable(salesQuery);
                    
                    decimal totalSales = 0;
                    if (dtSales.Rows.Count > 0 && dtSales.Rows[0][0] != DBNull.Value)
                    {
                        totalSales = Convert.ToDecimal(dtSales.Rows[0][0]);
                    }
                    
                    lblTotalSalesValue.Text = string.Format("${0:0.00}", totalSales);
                    
                    // Calculate Commission (e.g. 10%)
                    decimal totalCommission = totalSales * 0.10m;
                    lblTotalCommissionValue.Text = string.Format("${0:0.00}", totalCommission);

                    // Load Shop Ratings
                    string analyticsQuery = @"
                        SELECT 
                            s.ShopName AS [Shop Name],
                            COUNT(DISTINCT p.ProductID) AS [Total Products Listed],
                            ISNULL((SELECT SUM(oi.Subtotal) FROM OrderItems oi INNER JOIN Products p2 ON oi.ProductID = p2.ProductID WHERE p2.ShopID = s.ShopID), 0) AS [Total Earnings ($)],
                            ISNULL((SELECT AVG(CAST(r.Rating AS FLOAT)) FROM Reviews r INNER JOIN Products p3 ON r.ProductID = p3.ProductID WHERE p3.ShopID = s.ShopID), 0) AS [Average Rating]
                        FROM Shops s
                        LEFT JOIN Products p ON s.ShopID = p.ShopID
                        GROUP BY s.ShopID, s.ShopName
                        ORDER BY [Total Earnings ($)] DESC";
                    
                    DataTable dtRatings = da.ExecuteQueryTable(analyticsQuery);
                    dgvShopRatings.DataSource = dtRatings;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading dashboard stats: " + ex.Message);
            }
        }
    }
}
