using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartphoneCoverShop
{
    public class frmManageCategories : Form
    {
        private DataGridView dgvCategories;
        private TextBox txtCategoryName, txtDescription, txtSearch;
        private CheckBox chkIsActive;
        private Button btnAdd, btnUpdate, btnDelete, btnClear;
        private Label lblSearch, lblName, lblDesc, lblStatusFilter;
        private ComboBox cmbStatusFilter;
        private int selectedId = 0;

        public frmManageCategories()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.dgvCategories = new DataGridView();
            this.txtCategoryName = new TextBox();
            this.txtDescription = new TextBox();
            this.chkIsActive = new CheckBox();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnClear = new Button();
            this.txtSearch = new TextBox();
            this.lblSearch = new Label();
            this.lblName = new Label();
            this.lblDesc = new Label();
            this.lblStatusFilter = new Label();
            this.cmbStatusFilter = new ComboBox();

            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.SuspendLayout();

            // Labels
            this.lblName.AutoSize = true;
            this.lblName.Location = new Point(20, 20);
            this.lblName.Text = "Category Name:";

            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new Point(20, 55);
            this.lblDesc.Text = "Description:";

            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new Point(320, 20);
            this.lblSearch.Text = "Search:";

            this.lblStatusFilter.AutoSize = true;
            this.lblStatusFilter.Location = new Point(320, 55);
            this.lblStatusFilter.Text = "Filter Status:";

            // TextBoxes
            this.txtCategoryName.Location = new Point(130, 17);
            this.txtCategoryName.Size = new Size(160, 23);

            this.txtDescription.Location = new Point(130, 52);
            this.txtDescription.Size = new Size(160, 23);

            this.chkIsActive.Location = new Point(130, 90);
            this.chkIsActive.Text = "Is Active";
            this.chkIsActive.Checked = true;

            this.txtSearch.Location = new Point(380, 17);
            this.txtSearch.Size = new Size(200, 23);
            this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);

            this.cmbStatusFilter.Location = new Point(400, 52);
            this.cmbStatusFilter.Size = new Size(180, 23);
            this.cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.Items.AddRange(new string[] { "All", "Approved (1)", "Inactive (0)" });
            this.cmbStatusFilter.SelectedIndex = 0;
            this.cmbStatusFilter.SelectedIndexChanged += new EventHandler(this.cmbStatusFilter_SelectedIndexChanged);

            // Buttons
            this.btnAdd.Location = new Point(20, 120);
            this.btnAdd.Size = new Size(70, 30);
            this.btnAdd.Text = "Add";
            this.btnAdd.BackColor = Color.FromArgb(117, 86, 174);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            this.btnUpdate.Location = new Point(100, 120);
            this.btnUpdate.Size = new Size(70, 30);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.BackColor = Color.LightSkyBlue;
            this.btnUpdate.FlatStyle = FlatStyle.Flat;
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);

            this.btnDelete.Location = new Point(180, 120);
            this.btnDelete.Size = new Size(70, 30);
            this.btnDelete.Text = "Delete";
            this.btnDelete.BackColor = Color.LightCoral;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnClear.Location = new Point(260, 120);
            this.btnClear.Size = new Size(70, 30);
            this.btnClear.Text = "Clear";
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Click += new EventHandler(this.btnClear_Click);

            // DataGridView
            this.dgvCategories.Location = new Point(20, 170);
            this.dgvCategories.Size = new Size(560, 200);
            this.dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.AllowUserToAddRows = false;
            this.dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategories.CellClick += new DataGridViewCellEventHandler(this.dgvCategories_CellClick);

            // Form
            this.BackColor = Color.White;
            this.ClientSize = new Size(600, 400);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lblStatusFilter);
            this.Controls.Add(this.txtCategoryName);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cmbStatusFilter);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dgvCategories);
            this.Font = new Font("Nirmala UI", 9F);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Manage Categories";

            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void LoadData(string searchTerm = "")
        {
            try
            {
                using (DataAccess da = new DataAccess())
                {
                    string query = "SELECT CategoryID, CategoryName, Status FROM Categories WHERE 1=1";

                    // Status filter
                    if (cmbStatusFilter.SelectedIndex == 1) query += " AND Status = 1";
                    else if (cmbStatusFilter.SelectedIndex == 2) query += " AND Status = 0";

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        string safeTerm = searchTerm.Replace("'", "''");
                        query += string.Format(" AND CategoryName LIKE '%{0}%'", safeTerm);
                    }
                    DataTable dt = da.ExecuteQueryTable(query);
                    dgvCategories.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCategories.Rows[e.RowIndex];
                selectedId = Convert.ToInt32(row.Cells["CategoryID"].Value);
                txtCategoryName.Text = row.Cells["CategoryName"].Value.ToString();
                
                chkIsActive.Checked = Convert.ToInt32(row.Cells["Status"].Value) == 1;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Category Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (DataAccess da = new DataAccess())
                {
                    string safeName = txtCategoryName.Text.Replace("'", "''");
                    
                    int status = chkIsActive.Checked ? 1 : 0;
                    string query = string.Format("INSERT INTO Categories (CategoryName, Status) VALUES ('{0}', {1})", safeName, status);
                    da.ExecuteDMLQuery(query);
                    MessageBox.Show("Category added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("Please select a category to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Category Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (DataAccess da = new DataAccess())
                {
                    string safeName = txtCategoryName.Text.Replace("'", "''");
                    
                    int status = chkIsActive.Checked ? 1 : 0;
                    string query = string.Format("UPDATE Categories SET CategoryName = '{0}', Status = {1} WHERE CategoryID = {2}", safeName, status, selectedId);
                    da.ExecuteDMLQuery(query);
                    MessageBox.Show("Category updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("Please select a category to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this category?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    using (DataAccess da = new DataAccess())
                    {
                        string query = string.Format("DELETE FROM Categories WHERE CategoryID = {0}", selectedId);
                        da.ExecuteDMLQuery(query);
                        MessageBox.Show("Category deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting category. It might be in use by products.\nDetails: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtCategoryName.Clear();
            
            txtSearch.Clear();
            chkIsActive.Checked = true;
            dgvCategories.ClearSelection();
        }
    }
}

