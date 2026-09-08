using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SmartphoneCoverShop
{
    public partial class frmCheckout : Form
    {
        private int customerId;
        private Label lblTitle;
        private Label lblTotal;
        private Label lblCoupon;
        private TextBox txtCoupon;
        private Button btnApplyCoupon;
        private Button btnPay;
        private Button btnCancel;
        private decimal originalTotal = 0;
        private decimal discount = 0;
        private decimal finalTotal = 0;

        public frmCheckout(int customerId)
        {
            this.customerId = customerId;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblTotal = new Label();
            this.lblCoupon = new Label();
            this.txtCoupon = new TextBox();
            this.btnApplyCoupon = new Button();
            this.btnPay = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Text = "Checkout";

            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTotal.Location = new Point(20, 70);
            this.lblTotal.Text = "Total: ৳ 0.00";

            this.lblCoupon.AutoSize = true;
            this.lblCoupon.Location = new Point(20, 130);
            this.lblCoupon.Text = "Coupon Code (e.g. DISCOUNT10):";

            this.txtCoupon.Location = new Point(20, 150);
            this.txtCoupon.Size = new Size(150, 20);

            this.btnApplyCoupon.Location = new Point(180, 148);
            this.btnApplyCoupon.Size = new Size(75, 23);
            this.btnApplyCoupon.Text = "Apply";
            this.btnApplyCoupon.Click += new EventHandler(this.btnApplyCoupon_Click);

            this.btnPay.BackColor = Color.FromArgb(47, 126, 94);
            this.btnPay.FlatStyle = FlatStyle.Flat;
            this.btnPay.ForeColor = Color.White;
            this.btnPay.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnPay.Location = new Point(20, 220);
            this.btnPay.Size = new Size(120, 40);
            this.btnPay.Text = "Pay Now";
            this.btnPay.UseVisualStyleBackColor = false;
            this.btnPay.Click += new EventHandler(this.btnPay_Click);

            this.btnCancel.BackColor = Color.White;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.ForeColor = Color.IndianRed;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancel.Location = new Point(150, 220);
            this.btnCancel.Size = new Size(100, 40);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler((s, e) => this.Close());

            this.ClientSize = new Size(300, 300);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblCoupon);
            this.Controls.Add(this.txtCoupon);
            this.Controls.Add(this.btnApplyCoupon);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.btnCancel);
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Checkout";
            this.Load += new EventHandler(this.frmCheckout_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void frmCheckout_Load(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess();
            DataTable dt = da.ExecuteQueryTable("SELECT p.Price, c.Quantity FROM Cart c INNER JOIN Products p ON c.ProductID = p.ProductID WHERE c.CustomerID = " + customerId);
            foreach (DataRow row in dt.Rows)
            {
                originalTotal += Convert.ToDecimal(row["Price"]) * Convert.ToInt32(row["Quantity"]);
            }
            finalTotal = originalTotal;
            UpdateTotalLabel();
        }

        private void UpdateTotalLabel()
        {
            if (discount > 0)
            {
                lblTotal.Text = "Total: ৳ " + finalTotal.ToString("N2") + "\n(Discount applied: ৳ " + discount.ToString("N2") + ")";
            }
            else
            {
                lblTotal.Text = "Total: ৳ " + finalTotal.ToString("N2");
            }
        }

        private void btnApplyCoupon_Click(object sender, EventArgs e)
        {
            string code = txtCoupon.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Please enter a coupon code.");
                return;
            }

            DataAccess da = new DataAccess();
            // Check if coupon exists and is active
            string offerQuery = "SELECT ShopID, DiscountValue FROM Offers WHERE OfferName = '" + code.Replace("'", "''") + "' AND Status = 'Active'";
            DataTable dtOffer = da.ExecuteQueryTable(offerQuery);

            if (dtOffer.Rows.Count > 0)
            {
                int shopId = Convert.ToInt32(dtOffer.Rows[0]["ShopID"]);
                decimal discountPercentage = Convert.ToDecimal(dtOffer.Rows[0]["DiscountValue"]);

                // Calculate subtotal for items from this specific shop
                string cartShopQuery = "SELECT p.Price, c.Quantity FROM Cart c INNER JOIN Products p ON c.ProductID = p.ProductID WHERE c.CustomerID = " + customerId + " AND p.ShopID = " + shopId;
                DataTable dtCartShop = da.ExecuteQueryTable(cartShopQuery);

                decimal shopSubtotal = 0;
                foreach (DataRow row in dtCartShop.Rows)
                {
                    shopSubtotal += Convert.ToDecimal(row["Price"]) * Convert.ToInt32(row["Quantity"]);
                }

                if (shopSubtotal > 0)
                {
                    discount = shopSubtotal * (discountPercentage / 100m);
                    finalTotal = originalTotal - discount;
                    MessageBox.Show(discountPercentage + "% Discount applied to eligible items from the shop!");
                    UpdateTotalLabel();
                }
                else
                {
                    MessageBox.Show("This coupon is valid, but your cart doesn't contain any items from the shop that issued it.");
                    discount = 0;
                    finalTotal = originalTotal;
                    UpdateTotalLabel();
                }
            }
            else
            {
                MessageBox.Show("Invalid or expired coupon code.");
                discount = 0;
                finalTotal = originalTotal;
                UpdateTotalLabel();
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess();
            // Process order (dummy)
            da.ExecuteDMLQuery("INSERT INTO Orders (CustomerID, OrderDate, TotalAmount, PaymentMethod) VALUES (" + customerId + ", GETDATE(), " + finalTotal + ", 'Card')");
            
            // Get OrderID
            DataTable dt = da.ExecuteQueryTable("SELECT TOP 1 OrderID FROM Orders WHERE CustomerID = " + customerId + " ORDER BY OrderID DESC");
            if (dt.Rows.Count > 0)
            {
                int orderId = Convert.ToInt32(dt.Rows[0]["OrderID"]);
                // Copy cart to OrderItems (simplified)
                da.ExecuteDMLQuery("INSERT INTO OrderItems (OrderID, ProductID, Quantity, UnitPrice, Subtotal) SELECT " + orderId + ", c.ProductID, c.Quantity, p.Price, (p.Price * c.Quantity) FROM Cart c INNER JOIN Products p ON c.ProductID = p.ProductID WHERE c.CustomerID = " + customerId);
                // Clear cart
                da.ExecuteDMLQuery("DELETE FROM Cart WHERE CustomerID = " + customerId);
            }
            
            MessageBox.Show("Payment successful! Your order has been placed.");
            this.Close();
        }
    }
}

