using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SmartphoneCoverShop
{
    public partial class frmManageProducts : Form
    {
        private int loggedInUserId;
        private int shopId;

        public frmManageProducts(int userId)
        {
            InitializeComponent();
            loggedInUserId = userId;
        }

        private void frmManageProducts_Load(object sender, EventArgs e)
        {
            LoadShopId();
            LoadCategories();
            LoadProducts("");
        }

        private void LoadShopId()
        {
            using (DataAccess da = new DataAccess())
            {
                string query = "SELECT ShopID FROM Shops WHERE UserID = " + loggedInUserId;
                DataTable dt = da.ExecuteQueryTable(query);
                if (dt.Rows.Count > 0)
                {
                    shopId = Convert.ToInt32(dt.Rows[0]["ShopID"]);
                }
            }
        }

                private void LoadCategories()
        {
            using (DataAccess da = new DataAccess())
            {
                DataTable dt = da.ExecuteQueryTable("SELECT CategoryID, CategoryName FROM Categories");
                
                // For main form dropdown
                cmbCategory.DataSource = dt;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryID";
                
                // For filter dropdown
                DataTable dtFilter = dt.Copy();
                DataRow allRow = dtFilter.NewRow();
                allRow["CategoryID"] = 0;
                allRow["CategoryName"] = "All Categories";
                dtFilter.Rows.InsertAt(allRow, 0);
                
                cmbFilterCategory.DataSource = dtFilter;
                cmbFilterCategory.DisplayMember = "CategoryName";
                cmbFilterCategory.ValueMember = "CategoryID";
                cmbFilterCategory.SelectedIndex = 0;
            }
        }

                private void LoadProducts(string search)
        {
            if (shopId == 0) return;

            using (DataAccess da = new DataAccess())
            {
                string query = @"
                    SELECT 
                        p.ProductID, 
                        p.ProductName, 
                        c.CategoryName, 
                        p.Description, 
                        p.Price, 
                        p.StockQuantity
                    FROM Products p
                    INNER JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE p.ShopID = @ShopID
                ";

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND (p.ProductName LIKE @Search OR p.Description LIKE @Search)";
                }

                if (cmbFilterCategory.SelectedValue != null && cmbFilterCategory.SelectedValue is int)
                {
                    int catId = (int)cmbFilterCategory.SelectedValue;
                    if (catId > 0)
                    {
                        query += " AND p.CategoryID = @CatID";
                    }
                }

                da.Sqlcom.CommandText = query;
                da.Sqlcom.Parameters.AddWithValue("@ShopID", shopId);
                
                if (!string.IsNullOrEmpty(search))
                {
                    da.Sqlcom.Parameters.AddWithValue("@Search", "%" + search + "%");
                }
                
                if (cmbFilterCategory.SelectedValue != null && cmbFilterCategory.SelectedValue is int)
                {
                    int cid = (int)cmbFilterCategory.SelectedValue;
                    if (cid > 0)
                    {
                        da.Sqlcom.Parameters.AddWithValue("@CatID", cid);
                    }
                }

                DataTable dt = new DataTable();
                using (System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(da.Sqlcom))
                {
                    adapter.Fill(dt);
                }
                dgvProducts.DataSource = dt;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (shopId == 0)
            {
                MessageBox.Show("You must set up a shop profile first.");
                return;
            }

            if (cmbCategory.SelectedValue == null)
            {
                MessageBox.Show("Please select a valid category.");
                return;
            }

            decimal price;
            int stock;
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || 
                !decimal.TryParse(txtPrice.Text, out price) || 
                !int.TryParse(txtStock.Text, out stock))
            {
                MessageBox.Show("Please enter valid product details, price, and stock quantity.");
                return;
            }

            using (DataAccess da = new DataAccess())
            {
                string query = "INSERT INTO Products (ShopID, CategoryID, ProductName, Description, Price, StockQuantity) VALUES (@ShopID, @CategoryID, @ProductName, @Description, @Price, @StockQuantity)";
                da.Sqlcom = new SqlCommand(query, da.Sqlcon);
                
                da.Sqlcom.Parameters.AddWithValue("@ShopID", shopId);
                da.Sqlcom.Parameters.AddWithValue("@CategoryID", cmbCategory.SelectedValue);
                da.Sqlcom.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                da.Sqlcom.Parameters.AddWithValue("@Description", txtDescription.Text);
                da.Sqlcom.Parameters.AddWithValue("@Price", price);
                da.Sqlcom.Parameters.AddWithValue("@StockQuantity", stock);

                da.Sqlcom.ExecuteNonQuery();
                MessageBox.Show("Product added successfully!");
                ClearFields();
                LoadProducts("");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to update.");
                return;
            }

            decimal price;
            int stock;
            if (!decimal.TryParse(txtPrice.Text, out price) || !int.TryParse(txtStock.Text, out stock))
            {
                MessageBox.Show("Please enter valid price and stock quantity.");
                return;
            }

            int productId = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["ProductID"].Value);

            using (DataAccess da = new DataAccess())
            {
                string query = "UPDATE Products SET CategoryID = @CategoryID, ProductName = @ProductName, Description = @Description, Price = @Price, StockQuantity = @StockQuantity WHERE ProductID = @ProductID AND ShopID = @ShopID";
                da.Sqlcom = new SqlCommand(query, da.Sqlcon);

                da.Sqlcom.Parameters.AddWithValue("@CategoryID", cmbCategory.SelectedValue);
                da.Sqlcom.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                da.Sqlcom.Parameters.AddWithValue("@Description", txtDescription.Text);
                da.Sqlcom.Parameters.AddWithValue("@Price", price);
                da.Sqlcom.Parameters.AddWithValue("@StockQuantity", stock);
                da.Sqlcom.Parameters.AddWithValue("@ProductID", productId);
                da.Sqlcom.Parameters.AddWithValue("@ShopID", shopId);

                da.Sqlcom.ExecuteNonQuery();
                MessageBox.Show("Product updated successfully!");
                ClearFields();
                LoadProducts("");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            int productId = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["ProductID"].Value);

            var confirm = MessageBox.Show("Are you sure you want to permanently delete this product? This may fail if there are existing orders for this product.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                using (DataAccess da = new DataAccess())
                {
                    try
                    {
                        string query = "DELETE FROM Products WHERE ProductID = @ProductID AND ShopID = @ShopID";
                        da.Sqlcom = new SqlCommand(query, da.Sqlcon);
                        da.Sqlcom.Parameters.AddWithValue("@ProductID", productId);
                        da.Sqlcom.Parameters.AddWithValue("@ShopID", shopId);
                        da.Sqlcom.ExecuteNonQuery();
                        
                        MessageBox.Show("Product deleted successfully!");
                        ClearFields();
                        LoadProducts("");
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show("Cannot delete this product because it is part of an existing order or cart.");
                    }
                }
            }
        }

                private void cmbFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (shopId != 0 && cmbFilterCategory.SelectedValue != null)
            {
                LoadProducts(txtSearch.Text.Trim());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text.Trim());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            LoadProducts("");
        }

        private void ClearFields()
        {
            txtProductName.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            txtSearch.Clear();
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            dgvProducts.ClearSelection();
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvProducts.SelectedRows[0];
                txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
                txtStock.Text = row.Cells["StockQuantity"].Value.ToString();
                
                string categoryName = row.Cells["CategoryName"].Value.ToString();
                cmbCategory.SelectedIndex = cmbCategory.FindStringExact(categoryName);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

