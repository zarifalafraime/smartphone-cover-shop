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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Close();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LoggedInFullName))
            {
                string roleDisplay = LoggedInUserType?.ToUpper();
                lblWelcome.Text = "👤 " + LoggedInFullName + " [" + roleDisplay + "]";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
