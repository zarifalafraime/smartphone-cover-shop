using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace SmartphoneCoverShop
{
    public partial class frmManageProducts : Form
    {
        private int _userId;
        private int _shopId = 0;
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

        public frmManageProducts(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void frmManageProducts_Load(object sender, EventArgs e)
        {
            LoadShopId();
            LoadCategories();
            LoadProducts();
        }

        private void LoadShopId()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT ShopID FROM Shops WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", _userId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        _shopId = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Please create a Shop Profile first before managing products.", "Profile Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                }
            }
        }

        private void LoadCategories()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT CategoryID, CategoryName FROM Categories WHERE Status = 1";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    cmbCategory.DataSource = dt;
                    cmbCategory.DisplayMember = "CategoryName";
                    cmbCategory.ValueMember = "CategoryID";
                    cmbCategory.SelectedIndex = -1;
                }
            }
        }

        private void LoadProducts(string search = "")
        {
            if (_shopId == 0) return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"
                    SELECT p.ProductID, p.ProductName, c.CategoryName, p.Price, p.StockQuantity, p.Description 
                    FROM Products p
                    INNER JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE p.ShopID = @ShopID AND p.Status = 1";
                
                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND p.ProductName LIKE @Search";
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ShopID", _shopId);
                    if (!string.IsNullOrEmpty(search))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + search + "%");
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvProducts.DataSource = dt;
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO Products (ShopID, CategoryID, ProductName, Description, Price, StockQuantity, Status) VALUES (@ShopID, @CategoryID, @ProductName, @Description, @Price, @StockQuantity, 1)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ShopID", _shopId);
                    cmd.Parameters.AddWithValue("@CategoryID", cmbCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@StockQuantity", Convert.ToInt32(txtStock.Text));
                    cmd.ExecuteNonQuery();
                }
            }
            ClearFields();
            LoadProducts();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductID.Text))
            {
                MessageBox.Show("Please select a product to update.", "Select Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInput()) return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "UPDATE Products SET CategoryID = @CategoryID, ProductName = @ProductName, Description = @Description, Price = @Price, StockQuantity = @StockQuantity WHERE ProductID = @ProductID AND ShopID = @ShopID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", txtProductID.Text);
                    cmd.Parameters.AddWithValue("@ShopID", _shopId);
                    cmd.Parameters.AddWithValue("@CategoryID", cmbCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@StockQuantity", Convert.ToInt32(txtStock.Text));
                    cmd.ExecuteNonQuery();
                }
            }
            ClearFields();
            LoadProducts();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductID.Text))
            {
                MessageBox.Show("Please select a product to delete.", "Select Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this product?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // Soft delete
                    string query = "UPDATE Products SET Status = 0 WHERE ProductID = @ProductID AND ShopID = @ShopID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", txtProductID.Text);
                        cmd.Parameters.AddWithValue("@ShopID", _shopId);
                        cmd.ExecuteNonQuery();
                    }
                }
                ClearFields();
                LoadProducts();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text.Trim());
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
                txtProductID.Text = row.Cells["ProductID"].Value.ToString();
                txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                cmbCategory.Text = row.Cells["CategoryName"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
                txtStock.Text = row.Cells["StockQuantity"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
            }
        }

        private void ClearFields()
        {
            txtProductID.Clear();
            txtProductName.Clear();
            cmbCategory.SelectedIndex = -1;
            txtPrice.Clear();
            txtStock.Clear();
            txtDescription.Clear();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || cmbCategory.SelectedValue == null ||
                string.IsNullOrWhiteSpace(txtPrice.Text) || string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("Product Name, Category, Price, and Stock are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out _))
            {
                MessageBox.Show("Price must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtStock.Text, out _))
            {
                MessageBox.Show("Stock must be a valid integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
