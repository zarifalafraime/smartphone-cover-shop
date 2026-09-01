using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace SmartphoneCoverShop
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show(
                    "Please enter your Email and Password.",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                con.Open();

                string login = "SELECT UserID, FullName, Email, UserType, [Status] FROM Users " +
                               "WHERE (LOWER(Email) = LOWER(@email) OR LOWER(FullName) = LOWER(@email)) AND [Password] = @password";

                SqlCommand cmd = new SqlCommand(login, con);

                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int status = Convert.ToInt32(reader["Status"]);
                    if (status != 1)
                    {
                        con.Close();
                        MessageBox.Show(
                            "Your account is currently inactive. Please contact the Super Admin.",
                            "Account Inactive",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    string fullName = reader["FullName"].ToString();
                    string userType = reader["UserType"].ToString();
                    int userId = Convert.ToInt32(reader["UserID"]);

                    con.Close();

                    if (userType == "super_admin") {
                        new frmSuperAdminDashboard(userId, fullName, userType).Show();
                    } else if (userType == "admin") {
                        new frmAdminDashboard(userId, fullName, userType).Show();
                    } else if (userType == "customer") {
                        new frmCustomerDashboard(userId, fullName, userType).Show();
                    }
                    this.Hide();
                }
                else
                {
                    con.Close();
                    MessageBox.Show(
                        "Email and Password are incorrect. Please try again.",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    txtEmail.Text = "";
                    txtPassword.Text = "";
                    txtEmail.Focus();
                }
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                MessageBox.Show(
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtEmail.Focus();
        }

        private void clickRegister_Click(object sender, EventArgs e)
        {
            new frmRegister().Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Goodbye");
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
        }
    }
}




