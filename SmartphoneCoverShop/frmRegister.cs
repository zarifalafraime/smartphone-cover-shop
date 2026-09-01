using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace SmartphoneCoverShop
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtFullName.Text == "" ||
                txtEmail.Text == "" ||
                txtPassword.Text == "" ||
                txtConPassword.Text == "")
            {
                MessageBox.Show(
                    "Please fill in all required fields.",
                    "Register Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (txtPassword.Text == txtConPassword.Text)
            {
                try
                {
                    con.Open();

                    // Check if email already exists
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE LOWER(Email) = LOWER(@Email)";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (userExists > 0)
                    {
                        con.Close();
                        MessageBox.Show(
                            "An account with this Email already exists.",
                            "Register Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    string selectedRole = "customer";
                    int initialStatus = 1; // 1 = Approved (Default for customers)

                    if (cmbUserType.SelectedItem != null)
                    {
                        string val = cmbUserType.SelectedItem.ToString().ToLower();
                        if (val.Contains("admin") || val.Contains("shop"))
                        {
                            selectedRole = "admin";
                            initialStatus = 2; // 2 = Pending / No Status
                        }
                    }

                    string register =
                        "INSERT INTO Users (FullName, Email, [Password], UserType, Phone, CreatedAt, [Status]) " +
                        "VALUES (@FullName, @Email, @Password, @UserType, @Phone, GETDATE(), @Status)";

                    SqlCommand cmd = new SqlCommand(register, con);

                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@UserType", selectedRole);
                    cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@Status", initialStatus);

                    cmd.ExecuteNonQuery();

                    con.Close();

                    txtFullName.Text = "";
                    txtEmail.Text = "";
                    txtPhone.Text = "";
                    txtPassword.Text = "";
                    txtConPassword.Text = "";
                    if (cmbUserType.Items.Count > 0) cmbUserType.SelectedIndex = 0;
                    txtFullName.Focus();

                    MessageBox.Show(
                        "Your Account has been Successfully Created",
                        "Registration Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    new frmLogin().Show();
                    this.Hide();
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
            else
            {
                MessageBox.Show(
                    "Passwords do not match. Please re-enter.",
                    "Register Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPassword.Text = "";
                txtConPassword.Text = "";
                txtPassword.Focus();
            }
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtConPassword.PasswordChar = '•';
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtPassword.Text = "";
            txtConPassword.Text = "";
            if (cmbUserType.Items.Count > 0) cmbUserType.SelectedIndex = 0;
            txtFullName.Focus();
        }

        private void clickLogin_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Goodbye");
            Application.Exit();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            if (cmbUserType.Items.Count > 0)
            {
                cmbUserType.SelectedIndex = 0;
            }
        }
    }
}
