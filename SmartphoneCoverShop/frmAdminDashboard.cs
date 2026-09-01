using System;
using System.Windows.Forms;

namespace SmartphoneCoverShop
{
    public partial class frmAdminDashboard : Form
    {
        public int LoggedInUserId { get; set; }
        public string LoggedInFullName { get; set; }
        public string LoggedInUserType { get; set; }

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
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Close();
        }

        private void btnManageProfile_Click(object sender, EventArgs e)
        {
            new frmManageShopProfile(LoggedInUserId).ShowDialog();
        }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            new frmManageProducts(LoggedInUserId).ShowDialog();
        }
    }
}
