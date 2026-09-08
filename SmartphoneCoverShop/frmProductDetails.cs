using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SmartphoneCoverShop
{
    public partial class frmProductDetails : Form
    {
        private int productId;
        private int customerId;
        private string customerName;

        private Label lblName;
        private Label lblPrice;
        private Label lblStock;
        private TextBox txtDescription;
        private Button btnAddToCart;
        private Button btnClose;

        // Review section
        private DataGridView dgvReviews;
        private Label lblRating;
        private NumericUpDown numRating;
        private TextBox txtReview;
        private Button btnAddReview;

        public frmProductDetails(int productId, int customerId, string customerName)
        {
            this.productId = productId;
            this.customerId = customerId;
            this.customerName = customerName;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblName = new Label();
            this.lblPrice = new Label();
            this.lblStock = new Label();
            this.txtDescription = new TextBox();
            this.btnAddToCart = new Button();
            this.btnClose = new Button();
            
            this.dgvReviews = new DataGridView();
            this.lblRating = new Label();
            this.numRating = new NumericUpDown();
            this.txtReview = new TextBox();
            this.btnAddReview = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRating)).BeginInit();
            this.SuspendLayout();

            this.lblName.AutoSize = true;
            this.lblName.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblName.Location = new Point(20, 20);
            
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblPrice.Location = new Point(20, 60);

            this.lblStock.AutoSize = true;
            this.lblStock.Font = new Font("Segoe UI", 10F);
            this.lblStock.ForeColor = Color.Green;
            this.lblStock.Location = new Point(200, 60);

            this.txtDescription.Location = new Point(20, 90);
            this.txtDescription.Multiline = true;
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new Size(500, 80);
            this.txtDescription.ScrollBars = ScrollBars.Vertical;

            this.btnAddToCart.BackColor = Color.FromArgb(116, 86, 174);
            this.btnAddToCart.FlatStyle = FlatStyle.Flat;
            this.btnAddToCart.ForeColor = Color.White;
            this.btnAddToCart.Location = new Point(20, 180);
            this.btnAddToCart.Size = new Size(120, 35);
            this.btnAddToCart.Text = "Add to Cart";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            this.btnAddToCart.Click += new EventHandler(this.btnAddToCart_Click);

            Label lblCoupons = new Label();
            lblCoupons.AutoSize = true;
            lblCoupons.ForeColor = Color.FromArgb(47, 126, 94);
            lblCoupons.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCoupons.Location = new Point(150, 190);
            lblCoupons.Text = "Loading coupons...";

            this.dgvReviews.Location = new Point(20, 240);
            this.dgvReviews.Size = new Size(500, 150);
            this.dgvReviews.ReadOnly = true;
            this.dgvReviews.AllowUserToAddRows = false;
            this.dgvReviews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReviews.BackgroundColor = Color.White;

            this.lblRating.AutoSize = true;
            this.lblRating.Location = new Point(20, 410);
            this.lblRating.Text = "Rating (1-5):";

            this.numRating.Location = new Point(100, 408);
            this.numRating.Minimum = 1;
            this.numRating.Maximum = 5;
            this.numRating.Value = 5;

            this.txtReview.Location = new Point(20, 440);
            this.txtReview.Multiline = true;
            this.txtReview.Size = new Size(370, 50);

            this.btnAddReview.BackColor = Color.FromArgb(116, 86, 174);
            this.btnAddReview.FlatStyle = FlatStyle.Flat;
            this.btnAddReview.ForeColor = Color.White;
            this.btnAddReview.Location = new Point(400, 440);
            this.btnAddReview.Size = new Size(120, 50);
            this.btnAddReview.Text = "Post Review";
            this.btnAddReview.UseVisualStyleBackColor = false;
            this.btnAddReview.Click += new EventHandler(this.btnAddReview_Click);

            this.btnClose.Location = new Point(400, 510);
            this.btnClose.Size = new Size(120, 35);
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            this.ClientSize = new Size(550, 560);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnAddToCart);
            this.Controls.Add(lblCoupons);
            this.Controls.Add(this.dgvReviews);
            this.Controls.Add(this.lblRating);
            this.Controls.Add(this.numRating);
            this.Controls.Add(this.txtReview);
            this.Controls.Add(this.btnAddReview);
            this.Controls.Add(this.btnClose);
            this.Text = "Product Details";
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new EventHandler(this.frmProductDetails_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRating)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void frmProductDetails_Load(object sender, EventArgs e)
        {
            LoadProductData();
            LoadOffers();
            LoadReviews();
        }

        private void LoadProductData()
        {
            DataAccess da = new DataAccess();
            DataTable dt = da.ExecuteQueryTable("SELECT ProductName, Price, StockQuantity, Description FROM Products WHERE ProductID = " + productId);
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                lblName.Text = r["ProductName"].ToString();
                lblPrice.Text = "৳ " + r["Price"].ToString();
                lblStock.Text = r["StockQuantity"].ToString() + " in stock";
                txtDescription.Text = r["Description"].ToString();
            }
        }

        private void LoadOffers()
        {
            DataAccess da = new DataAccess();
            DataTable dt = da.ExecuteQueryTable("SELECT o.OfferName, o.DiscountValue FROM Offers o INNER JOIN Products p ON o.ShopID = p.ShopID WHERE p.ProductID = " + productId + " AND o.Status = 'Active'");
            
            string couponsText = "";
            if (dt.Rows.Count > 0)
            {
                couponsText = "Shop Coupons: ";
                foreach (DataRow row in dt.Rows)
                {
                    couponsText += row["OfferName"].ToString() + " (" + Convert.ToDecimal(row["DiscountValue"]).ToString("0.##") + "% off)  ";
                }
            }
            else
            {
                couponsText = "No coupons available.";
            }

            foreach (Control c in this.Controls)
            {
                if (c is Label && c.ForeColor == Color.FromArgb(47, 126, 94))
                {
                    c.Text = couponsText;
                    break;
                }
            }
        }

        private void LoadReviews()
        {
            DataAccess da = new DataAccess();
            DataTable dt = da.ExecuteQueryTable("SELECT u.FullName as Customer, r.Rating, r.Comment FROM Reviews r INNER JOIN Users u ON r.CustomerID = u.UserID WHERE r.ProductID = " + productId);
            dgvReviews.DataSource = dt;
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess();

            // Check stock
            DataTable dtProduct = da.ExecuteQueryTable("SELECT StockQuantity, ProductName FROM Products WHERE ProductID = " + productId);
            if (dtProduct.Rows.Count > 0)
            {
                int stock = Convert.ToInt32(dtProduct.Rows[0]["StockQuantity"]);
                string name = dtProduct.Rows[0]["ProductName"].ToString();

                // Check if already in cart
                DataTable dtCart = da.ExecuteQueryTable("SELECT CartID, Quantity FROM Cart WHERE CustomerID = " + customerId + " AND ProductID = " + productId);
                if (dtCart.Rows.Count > 0)
                {
                    int currentQty = Convert.ToInt32(dtCart.Rows[0]["Quantity"]);
                    int cartId = Convert.ToInt32(dtCart.Rows[0]["CartID"]);

                    if (currentQty + 1 > stock)
                    {
                        MessageBox.Show("Cannot add to cart. You already have " + currentQty + " in your cart and only " + stock + " are available for '" + name + "'.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        da.ExecuteDMLQuery("UPDATE Cart SET Quantity = Quantity + 1 WHERE CartID = " + cartId);
                        MessageBox.Show("Product quantity increased in cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (stock < 1)
                    {
                        MessageBox.Show("Cannot add to cart. '" + name + "' is out of stock.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        da.ExecuteDMLQuery("INSERT INTO Cart (CustomerID, ProductID, Quantity) VALUES (" + customerId + ", " + productId + ", 1)");
                        MessageBox.Show("Product added to cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnAddReview_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReview.Text))
            {
                MessageBox.Show("Please enter a review.");
                return;
            }
            int rating = (int)numRating.Value;
            DataAccess da = new DataAccess();
            string query = string.Format("INSERT INTO Reviews (CustomerID, ProductID, Rating, Comment) VALUES ({0}, {1}, {2}, '{3}')",
                customerId, productId, rating, txtReview.Text.Replace("'", "''"));
            da.ExecuteDMLQuery(query);
            txtReview.Clear();
            LoadReviews();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}




