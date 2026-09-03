using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartphoneCoverShop
{
    public class frmManageUsers : Form
    {
        private DataGridView dgvUsers;
        private TextBox txtFullName, txtEmail, txtPhone, txtPassword, txtSearch;
        private ComboBox cmbUserType, cmbStatus;
        private Button btnAdd, btnUpdate, btnDelete, btnClear;
        private Label lblName, lblEmail, lblPhone, lblPassword, lblType, lblStatus, lblSearch;
        private int selectedId = 0;

        public frmManageUsers()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.dgvUsers = new DataGridView();
            this.txtFullName = new TextBox();
            this.txtEmail = new TextBox();
            this.txtPhone = new TextBox();
            this.txtPassword = new TextBox();
            this.txtSearch = new TextBox();
            this.cmbUserType = new ComboBox();
            this.cmbStatus = new ComboBox();
            
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnClear = new Button();
            
            this.lblName = new Label();
            this.lblEmail = new Label();
            this.lblPhone = new Label();
            this.lblPassword = new Label();
            this.lblType = new Label();
            this.lblStatus = new Label();
            this.lblSearch = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();

            // Labels
            this.lblName.Text = "Full Name:";
            this.lblName.Location = new Point(20, 20);
            
            this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new Point(20, 55);

            this.lblPhone.Text = "Phone:";
            this.lblPhone.Location = new Point(20, 90);

            this.lblPassword.Text = "Password:";
            this.lblPassword.Location = new Point(320, 20);

            this.lblType.Text = "Role:";
            this.lblType.Location = new Point(320, 55);

            this.lblStatus.Text = "Status:";
            this.lblStatus.Location = new Point(320, 90);

            this.lblSearch.Text = "Search Name/Email:";
            this.lblSearch.Location = new Point(20, 180);
            this.lblSearch.Width = 120;

            // Inputs
            this.txtFullName.Location = new Point(100, 17);
            this.txtFullName.Width = 180;

            this.txtEmail.Location = new Point(100, 52);
            this.txtEmail.Width = 180;

            this.txtPhone.Location = new Point(100, 87);
            this.txtPhone.Width = 180;

            this.txtPassword.Location = new Point(400, 17);
            this.txtPassword.Width = 180;

            this.cmbUserType.Location = new Point(400, 52);
            this.cmbUserType.Width = 180;
            this.cmbUserType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbUserType.Items.AddRange(new string[] { "customer", "admin", "super_admin" });

            this.cmbStatus.Location = new Point(400, 87);
            this.cmbStatus.Width = 180;
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatus.Items.AddRange(new string[] { "0 - Inactive/Rejected", "1 - Active/Approved", "2 - Pending" });

            this.txtSearch.Location = new Point(150, 177);
            this.txtSearch.Width = 250;
            this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);

            // Buttons
            this.btnAdd.Text = "Add";
            this.btnAdd.Location = new Point(20, 130);
            this.btnAdd.Size = new Size(80, 30);
            this.btnAdd.BackColor = Color.FromArgb(117, 86, 174);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            this.btnUpdate.Text = "Update";
            this.btnUpdate.Location = new Point(110, 130);
            this.btnUpdate.Size = new Size(80, 30);
            this.btnUpdate.BackColor = Color.LightSkyBlue;
            this.btnUpdate.FlatStyle = FlatStyle.Flat;
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);

            this.btnDelete.Text = "Delete";
            this.btnDelete.Location = new Point(200, 130);
            this.btnDelete.Size = new Size(80, 30);
            this.btnDelete.BackColor = Color.LightCoral;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnClear.Text = "Clear";
            this.btnClear.Location = new Point(290, 130);
            this.btnClear.Size = new Size(80, 30);
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Click += new EventHandler(this.btnClear_Click);

            // DataGridView
            this.dgvUsers.Location = new Point(20, 220);
            this.dgvUsers.Size = new Size(740, 220);
            this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.CellClick += new DataGridViewCellEventHandler(this.dgvUsers_CellClick);

            // Form Properties
            this.ClientSize = new Size(780, 460);
            this.BackColor = Color.White;
            this.Font = new Font("Nirmala UI", 9F);
            this.Text = "Manage Platform Users";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(lblName);
            this.Controls.Add(txtFullName);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblPhone);
            this.Controls.Add(txtPhone);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(lblType);
            this.Controls.Add(cmbUserType);
            this.Controls.Add(lblStatus);
            this.Controls.Add(cmbStatus);
            this.Controls.Add(lblSearch);
            this.Controls.Add(txtSearch);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnClear);
            this.Controls.Add(dgvUsers);

            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadData(string searchTerm = "")
        {
            try
            {
                using (DataAccess da = new DataAccess())
                {
                    string query = "SELECT UserID, FullName, Email, Password, UserType, Phone, Status, CreatedAt FROM Users";
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        string safeTerm = searchTerm.Replace("'", "''");
                        query += string.Format(" WHERE FullName LIKE '%{0}%' OR Email LIKE '%{0}%'", safeTerm);
                    }
                    DataTable dt = da.ExecuteQueryTable(query);
                    dgvUsers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
                selectedId = Convert.ToInt32(row.Cells["UserID"].Value);
                txtFullName.Text = row.Cells["FullName"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();
                cmbUserType.SelectedItem = row.Cells["UserType"].Value.ToString();
                
                int st = Convert.ToInt32(row.Cells["Status"].Value);
                cmbStatus.SelectedIndex = st == 0 ? 0 : (st == 1 ? 1 : 2);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || cmbUserType.SelectedItem == null || cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please fill all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (DataAccess da = new DataAccess())
                {
                    string safeName = txtFullName.Text.Replace("'", "''");
                    string safeEmail = txtEmail.Text.Replace("'", "''");
                    string safePhone = txtPhone.Text.Replace("'", "''");
                    string safePass = txtPassword.Text.Replace("'", "''");
                    int status = cmbStatus.SelectedIndex == 0 ? 0 : (cmbStatus.SelectedIndex == 1 ? 1 : 2);

                    string query = string.Format("INSERT INTO Users (FullName, Email, Password, UserType, Phone, Status, CreatedAt) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', {5}, GETDATE())",
                        safeName, safeEmail, safePass, cmbUserType.SelectedItem.ToString(), safePhone, status);
                    da.ExecuteDMLQuery(query);
                    MessageBox.Show("User added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("Please select a user to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (DataAccess da = new DataAccess())
                {
                    string safeName = txtFullName.Text.Replace("'", "''");
                    string safeEmail = txtEmail.Text.Replace("'", "''");
                    string safePhone = txtPhone.Text.Replace("'", "''");
                    string safePass = txtPassword.Text.Replace("'", "''");
                    int status = cmbStatus.SelectedIndex == 0 ? 0 : (cmbStatus.SelectedIndex == 1 ? 1 : 2);

                    string query = string.Format("UPDATE Users SET FullName='{0}', Email='{1}', Password='{2}', UserType='{3}', Phone='{4}', Status={5} WHERE UserID={6}",
                        safeName, safeEmail, safePass, cmbUserType.SelectedItem.ToString(), safePhone, status, selectedId);
                    da.ExecuteDMLQuery(query);
                    MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("Please select a user to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this user?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    using (DataAccess da = new DataAccess())
                    {
                        string query = string.Format("DELETE FROM Users WHERE UserID={0}", selectedId);
                        da.ExecuteDMLQuery(query);
                        MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot delete user because they have associated records (e.g. Orders/Shops).", "Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            selectedId = 0;
            txtFullName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtPassword.Clear();
            txtSearch.Clear();
            cmbUserType.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            dgvUsers.ClearSelection();
        }
    }
}
