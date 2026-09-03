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
                    string salesQuery = "SELECT SUM(TotalAmount) FROM Orders WHERE Status != 'Cancelled'";
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
                    string ratingsQuery = @"
                        SELECT s.ShopName AS [Shop Name], 
                               ISNULL(AVG(CAST(r.Rating AS FLOAT)), 0) AS [Average Rating], 
                               COUNT(r.ReviewID) AS [Total Reviews]
                        FROM Shops s
                        LEFT JOIN Products p ON s.ShopID = p.ShopID
                        LEFT JOIN Reviews r ON p.ProductID = r.ProductID
                        GROUP BY s.ShopName
                        ORDER BY [Average Rating] DESC";
                    
                    DataTable dtRatings = da.ExecuteQueryTable(ratingsQuery);
                    dgvShopRatings.DataSource = dtRatings;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dashboard stats: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
