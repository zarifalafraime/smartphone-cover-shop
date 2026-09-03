using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartphoneCoverShop
{
    public class frmManageReviews : Form
    {
        private DataGridView dgvReviews;
        private Button btnDelete, btnClose;
        private TextBox txtSearch;
        private Label lblSearch;
        private int selectedReviewId = 0;

        public frmManageReviews()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.dgvReviews = new DataGridView();
            this.btnDelete = new Button();
            this.btnClose = new Button();
            this.txtSearch = new TextBox();
            this.lblSearch = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).BeginInit();
            this.SuspendLayout();

            this.lblSearch.Text = "Search by Shop/Product/Customer:";
            this.lblSearch.Location = new Point(20, 20);
            this.lblSearch.AutoSize = true;

            this.txtSearch.Location = new Point(230, 17);
            this.txtSearch.Width = 250;
            this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);

            this.btnDelete.Text = "Delete Selected Review";
            this.btnDelete.Location = new Point(20, 390);
            this.btnDelete.Size = new Size(160, 35);
            this.btnDelete.BackColor = Color.LightCoral;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnClose.Text = "Close";
            this.btnClose.Location = new Point(600, 390);
            this.btnClose.Size = new Size(160, 35);
            this.btnClose.BackColor = Color.LightGray;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            this.dgvReviews.Location = new Point(20, 50);
            this.dgvReviews.Size = new Size(740, 330);
            this.dgvReviews.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvReviews.ReadOnly = true;
            this.dgvReviews.AllowUserToAddRows = false;
            this.dgvReviews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReviews.CellClick += new DataGridViewCellEventHandler(this.dgvReviews_CellClick);

            this.ClientSize = new Size(780, 440);
            this.BackColor = Color.White;
            this.Font = new Font("Nirmala UI", 9F);
            this.Text = "Manage Customer Reviews";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(lblSearch);
            this.Controls.Add(txtSearch);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnClose);
            this.Controls.Add(dgvReviews);

            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadData(string searchTerm = "")
        {
            try
            {
                using (DataAccess da = new DataAccess())
                {
                    string query = @"
                        SELECT r.ReviewID, c.FullName AS CustomerName, s.ShopName, p.ProductName, r.Rating, r.Comment, r.CreatedAt
                        FROM Reviews r
                        INNER JOIN Users c ON r.CustomerID = c.UserID
                        INNER JOIN Products p ON r.ProductID = p.ProductID
                        INNER JOIN Shops s ON p.ShopID = s.ShopID";
                    
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        string safeTerm = searchTerm.Replace("'", "''");
                        query += string.Format(" WHERE s.ShopName LIKE '%{0}%' OR p.ProductName LIKE '%{0}%' OR c.FullName LIKE '%{0}%' OR r.Comment LIKE '%{0}%'", safeTerm);
                    }
                    DataTable dt = da.ExecuteQueryTable(query);
                    dgvReviews.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reviews: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void dgvReviews_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvReviews.Rows[e.RowIndex];
                selectedReviewId = Convert.ToInt32(row.Cells["ReviewID"].Value);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedReviewId == 0)
            {
                MessageBox.Show("Please select a review to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this customer review? This action cannot be undone.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    using (DataAccess da = new DataAccess())
                    {
                        string query = string.Format("DELETE FROM Reviews WHERE ReviewID={0}", selectedReviewId);
                        da.ExecuteDMLQuery(query);
                        MessageBox.Show("Review deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        selectedReviewId = 0;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting review: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
