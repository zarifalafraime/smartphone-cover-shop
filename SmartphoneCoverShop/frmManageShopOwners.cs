using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartphoneCoverShop
{
    public class frmManageShopOwners : Form
    {
        private DataGridView dgvShopOwners;
        private Button btnApprove;
        private Button btnReject;
        private Button btnClose;

        private TextBox txtSearch;
        private Label lblSearch;

        public frmManageShopOwners()
        {
            InitializeComponent();
            LoadData("");
        }

        private void InitializeComponent()
        {
            this.dgvShopOwners = new DataGridView();
            this.btnApprove = new Button();
            this.btnReject = new Button();
            this.btnClose = new Button();
            this.txtSearch = new TextBox();
            this.lblSearch = new Label();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvShopOwners)).BeginInit();
            this.SuspendLayout();
            
            // lblSearch
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new Point(12, 18);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new Size(130, 15);
            this.lblSearch.TabIndex = 4;
            this.lblSearch.Text = "Search by Name/Email:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new Point(150, 15);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new Size(200, 23);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);
            
            // 
            // dgvShopOwners
            // 
            this.dgvShopOwners.AllowUserToAddRows = false;
            this.dgvShopOwners.AllowUserToDeleteRows = false;
            this.dgvShopOwners.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShopOwners.Location = new Point(12, 50);
            this.dgvShopOwners.Name = "dgvShopOwners";
            this.dgvShopOwners.ReadOnly = true;
            this.dgvShopOwners.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvShopOwners.MultiSelect = false;
            this.dgvShopOwners.Size = new Size(600, 262);
            this.dgvShopOwners.TabIndex = 0;
            this.dgvShopOwners.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 
            // btnApprove
            // 
            this.btnApprove.BackColor = Color.LightGreen;
            this.btnApprove.FlatStyle = FlatStyle.Flat;
            this.btnApprove.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold);
            this.btnApprove.Location = new Point(12, 330);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new Size(120, 35);
            this.btnApprove.TabIndex = 1;
            this.btnApprove.Text = "Approve";
            this.btnApprove.UseVisualStyleBackColor = false;
            this.btnApprove.Click += new EventHandler(this.btnApprove_Click);
            // 
            // btnReject
            // 
            this.btnReject.BackColor = Color.LightCoral;
            this.btnReject.FlatStyle = FlatStyle.Flat;
            this.btnReject.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold);
            this.btnReject.Location = new Point(150, 330);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new Size(120, 35);
            this.btnReject.TabIndex = 2;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = false;
            this.btnReject.Click += new EventHandler(this.btnReject_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = Color.LightGray;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold);
            this.btnClose.Location = new Point(492, 330);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(120, 35);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            // 
            // frmManageShopOwners
            // 
            this.BackColor = Color.White;
            this.ClientSize = new Size(624, 381);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.dgvShopOwners);
            this.Font = new Font("Nirmala UI", 9F);
            this.Name = "frmManageShopOwners";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Manage Shop Owners";
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvShopOwners)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void LoadData(string searchTerm = "")
        {
            try
            {
                using (DataAccess da = new DataAccess())
                {
                    string query = "SELECT UserID, FullName, Email, Phone, Status, CreatedAt FROM Users WHERE UserType = 'admin' AND Status = 2";
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        // Note: A parameterized query is safer, but adapting to the existing DataAccess ExecuteQueryTable here:
                        string safeTerm = searchTerm.Replace("'", "''");
                        query += string.Format(" AND (FullName LIKE '%{0}%' OR Email LIKE '%{0}%')", safeTerm);
                    }
                    DataTable dt = da.ExecuteQueryTable(query);
                    dgvShopOwners.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading shop owners: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatus(int status)
        {
            if (dgvShopOwners.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dgvShopOwners.SelectedRows[0].Cells["UserID"].Value);
                string action = status == 1 ? "approve" : "reject";
                
                DialogResult dialogResult = MessageBox.Show(string.Format("Are you sure you want to {0} this shop owner?", action), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        using (DataAccess da = new DataAccess())
                        {
                            string query = status == 1 
                                ? string.Format("UPDATE Users SET Status = 1 WHERE UserID = {0}", userId)
                                : string.Format("DELETE FROM Users WHERE UserID = {0}", userId);
                            da.ExecuteDMLQuery(query);
                        }
                        
                        MessageBox.Show(string.Format("Account successfully {0}ed.", action), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error updating status: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a shop owner from the list first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            UpdateStatus(1);
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            UpdateStatus(0); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
