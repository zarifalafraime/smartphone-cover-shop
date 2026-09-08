using System;
using System.Windows.Forms;

namespace SmartphoneCoverShop
{
    public partial class frmCustomerDashboard : Form
    {
        public int LoggedInUserId { get; set; }
        public string LoggedInFullName { get; set; }
        public string LoggedInUserType { get; set; }

        public frmCustomerDashboard()
        {
            InitializeComponent();
        }

        public frmCustomerDashboard(int userId, string fullName, string userType)
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
                lblWelcome.Text = "Welcome, " + LoggedInFullName;
            }
            LoadCoupons();
        }

        private void LoadCoupons()
        {
            DataAccess da = new DataAccess();
            string query = "SELECT s.ShopName, o.OfferName AS 'Coupon Code', o.DiscountValue AS 'Discount (%)' " +
                           "FROM Offers o INNER JOIN Shops s ON o.ShopID = s.ShopID " +
                           "WHERE o.Status = 'Active'";
            System.Data.DataTable dt = da.ExecuteQueryTable(query);
            
            dgvCoupons.DataSource = dt;
            
            // Apply similar styling as other grids
            dgvCoupons.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCoupons.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(116, 86, 174);
            dgvCoupons.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvCoupons.DefaultCellStyle.Padding = new Padding(5);
            dgvCoupons.RowTemplate.Height = 35;
            dgvCoupons.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCoupons.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 249, 252);
            dgvCoupons.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            dgvCoupons.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvCoupons.EnableHeadersVisualStyles = false;
            dgvCoupons.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Close();
        }

        private void btnBrowseProducts_Click(object sender, EventArgs e)
        {
            new frmBrowseProducts(LoggedInUserId, LoggedInFullName).ShowDialog();
        }

        private void btnMyCart_Click(object sender, EventArgs e)
        {
            new frmCustomerCart(LoggedInUserId).ShowDialog();
        }

        private void btnOrderHistory_Click(object sender, EventArgs e)
        {
            new frmOrderHistory(LoggedInUserId).ShowDialog();
        }
    }
}
