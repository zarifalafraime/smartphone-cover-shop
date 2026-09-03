using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SmartphoneCoverShop
{
    public partial class frmBrowseProducts : Form
    {
        private int customerId;
        private string customerName;

        private DataGridView dgvProducts;
        private Label lblTitle;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnAddToCart;
        private Button btnClose;

        private Button btnViewDetails;

        private Label lblCategory;
        private ComboBox cmbCategory;

        public frmBrowseProducts(int customerId, string customerName)
        {
            this.customerId = customerId;
            this.customerName = customerName;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.dgvProducts = new DataGridView();
            this.lblSearch = new Label();
            this.txtSearch = new TextBox();
            this.lblCategory = new Label();
            this.cmbCategory = new ComboBox();
            this.btnAddToCart = new Button();
            this.btnViewDetails = new Button();
            this.btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.SuspendLayout();
            
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Text = "Browse Products";
            
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new Font("Segoe UI", 10F);
            this.lblSearch.Location = new Point(220, 30);
            this.lblSearch.Text = "Search:";
            
            this.txtSearch.Font = new Font("Segoe UI", 10F);
            this.txtSearch.Location = new Point(280, 27);
            this.txtSearch.Size = new Size(150, 25);
            this.txtSearch.TextChanged += new EventHandler(this.Filter_Changed);
            
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new Font("Segoe UI", 10F);
            this.lblCategory.Location = new Point(450, 30);
            this.lblCategory.Text = "Category:";
            
            this.cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new Font("Segoe UI", 10F);
            this.cmbCategory.Location = new Point(520, 27);
            this.cmbCategory.Size = new Size(150, 25);
            this.cmbCategory.SelectedIndexChanged += new EventHandler(this.Filter_Changed);
            
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Location = new Point(25, 70);
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new Size(735, 300);
            this.dgvProducts.BackgroundColor = Color.White;
            this.dgvProducts.BorderStyle = BorderStyle.None;
            this.dgvProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(116, 86, 174);
            this.dgvProducts.DefaultCellStyle.SelectionForeColor = Color.White;
            this.dgvProducts.DefaultCellStyle.Padding = new Padding(5);
            this.dgvProducts.RowTemplate.Height = 40;
            this.dgvProducts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252);
            this.dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            this.dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.dgvProducts.EnableHeadersVisualStyles = false;
            this.dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            this.btnAddToCart.BackColor = Color.FromArgb(116, 86, 174);
            this.btnAddToCart.FlatStyle = FlatStyle.Flat;
            this.btnAddToCart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAddToCart.ForeColor = Color.White;
            this.btnAddToCart.Location = new Point(25, 390);
            this.btnAddToCart.Size = new Size(120, 35);
            this.btnAddToCart.Text = "Add to Cart";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            this.btnAddToCart.Click += new EventHandler(this.btnAddToCart_Click);
            
            this.btnViewDetails.BackColor = Color.FromArgb(116, 86, 174);
            this.btnViewDetails.FlatStyle = FlatStyle.Flat;
            this.btnViewDetails.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnViewDetails.ForeColor = Color.White;
            this.btnViewDetails.Location = new Point(160, 390);
            this.btnViewDetails.Size = new Size(120, 35);
            this.btnViewDetails.Text = "View Details";
            this.btnViewDetails.UseVisualStyleBackColor = false;
            this.btnViewDetails.Click += new EventHandler(this.btnViewDetails_Click);

            this.btnClose.BackColor = Color.White;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnClose.ForeColor = Color.FromArgb(116, 86, 174);
            this.btnClose.Location = new Point(295, 390);
            this.btnClose.Size = new Size(120, 35);
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(784, 451);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnViewDetails);
            this.Controls.Add(this.btnAddToCart);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Browse Products";
            this.Load += new EventHandler(this.frmBrowseProducts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void frmBrowseProducts_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadProducts();
        }

        private void LoadCategories()
        {
            DataAccess da = new DataAccess();
            DataTable dt = da.ExecuteQueryTable("SELECT CategoryID, CategoryName FROM Categories WHERE Status = 1");
            DataRow allRow = dt.NewRow();
            allRow["CategoryID"] = 0;
            allRow["CategoryName"] = "All Categories";
            dt.Rows.InsertAt(allRow, 0);
            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
        }

        private void LoadProducts(string search = "", int categoryId = 0)
        {
            string query = "SELECT ProductID, ProductName, Price, StockQuantity, Description FROM Products WHERE Status = 1";
            if (!string.IsNullOrEmpty(search))
            {
                query += " AND ProductName LIKE '%" + search.Replace("'", "''") + "%'";
            }
            if (categoryId > 0)
            {
                query += " AND CategoryID = " + categoryId;
            }
            DataAccess da = new DataAccess();
            DataTable dt = da.ExecuteQueryTable(query);
            dgvProducts.DataSource = dt;
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            int catId = 0;
            if (cmbCategory.SelectedValue != null && int.TryParse(cmbCategory.SelectedValue.ToString(), out int val))
            {
                catId = val;
            }
            LoadProducts(txtSearch.Text, catId);
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                int productId = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["ProductID"].Value);
                string query = "INSERT INTO Cart (CustomerID, ProductID, Quantity, AddedDate) VALUES (" + customerId + ", " + productId + ", 1, GETDATE())";
                DataAccess da = new DataAccess();
                da.ExecuteDMLQuery(query);
                MessageBox.Show("Product added to cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a product first.", "Select Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                int productId = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["ProductID"].Value);
                new frmProductDetails(productId, customerId, customerName).ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a product first.", "Select Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
