using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SmartphoneCoverShop
{
    public partial class frmCustomerCart : Form
    {
        private int customerId;
        
        private DataGridView dgvCart;
        private Label lblTitle;
        private Button btnRemove;
        private Button btnCheckout;
        private Button btnClose;
        private Label lblTotal;

        private Label lblQuantity;
        private NumericUpDown numQuantity;
        private Button btnUpdateQty;

        public frmCustomerCart(int customerId)
        {
            this.customerId = customerId;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.dgvCart = new DataGridView();
            this.btnRemove = new Button();
            this.btnCheckout = new Button();
            this.btnClose = new Button();
            this.lblTotal = new Label();
            
            this.lblQuantity = new Label();
            this.numQuantity = new NumericUpDown();
            this.btnUpdateQty = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();
            
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Text = "My Cart";
            
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Location = new Point(25, 70);
            this.dgvCart.ReadOnly = true;
            this.dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new Size(735, 300);
            this.dgvCart.BackgroundColor = Color.White;
            this.dgvCart.BorderStyle = BorderStyle.None;
            this.dgvCart.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCart.DefaultCellStyle.SelectionBackColor = Color.FromArgb(116, 86, 174);
            this.dgvCart.DefaultCellStyle.SelectionForeColor = Color.White;
            this.dgvCart.DefaultCellStyle.Padding = new Padding(5);
            this.dgvCart.RowTemplate.Height = 40;
            this.dgvCart.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvCart.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252);
            this.dgvCart.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            this.dgvCart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.dgvCart.EnableHeadersVisualStyles = false;
            this.dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.SelectionChanged += new EventHandler(this.dgvCart_SelectionChanged);
            
            this.btnRemove.BackColor = Color.IndianRed;
            this.btnRemove.FlatStyle = FlatStyle.Flat;
            this.btnRemove.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRemove.ForeColor = Color.White;
            this.btnRemove.Location = new Point(25, 390);
            this.btnRemove.Size = new Size(120, 35);
            this.btnRemove.Text = "Remove Item";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
            
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new Point(160, 400);
            this.lblQuantity.Text = "Qty:";
            
            this.numQuantity.Location = new Point(190, 398);
            this.numQuantity.Size = new Size(50, 20);
            this.numQuantity.Minimum = 1;

            this.btnUpdateQty.BackColor = Color.FromArgb(116, 86, 174);
            this.btnUpdateQty.FlatStyle = FlatStyle.Flat;
            this.btnUpdateQty.ForeColor = Color.White;
            this.btnUpdateQty.Location = new Point(250, 390);
            this.btnUpdateQty.Size = new Size(80, 35);
            this.btnUpdateQty.Text = "Update";
            this.btnUpdateQty.UseVisualStyleBackColor = false;
            this.btnUpdateQty.Click += new EventHandler(this.btnUpdateQty_Click);

            this.btnCheckout.BackColor = Color.FromArgb(47, 126, 94);
            this.btnCheckout.FlatStyle = FlatStyle.Flat;
            this.btnCheckout.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnCheckout.ForeColor = Color.White;
            this.btnCheckout.Location = new Point(340, 390);
            this.btnCheckout.Size = new Size(120, 35);
            this.btnCheckout.Text = "Checkout";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new EventHandler(this.btnCheckout_Click);

            this.btnClose.BackColor = Color.White;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnClose.ForeColor = Color.FromArgb(116, 86, 174);
            this.btnClose.Location = new Point(470, 390);
            this.btnClose.Size = new Size(80, 35);
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTotal.Location = new Point(570, 390);
            this.lblTotal.Text = "Total: ৳ 0";

            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(784, 451);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCheckout);
            this.Controls.Add(this.btnUpdateQty);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "My Cart";
            this.Load += new EventHandler(this.frmCustomerCart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void frmCustomerCart_Load(object sender, EventArgs e)
        {
            LoadCart();
        }

        private void LoadCart()
        {
            string query = "SELECT c.CartID, p.ProductName, p.Price, c.Quantity, (p.Price * c.Quantity) AS Subtotal FROM Cart c INNER JOIN Products p ON c.ProductID = p.ProductID WHERE c.CustomerID = " + customerId;
            DataAccess da = new DataAccess();
            DataTable dt = da.ExecuteQueryTable(query);
            dgvCart.DataSource = dt;
            
            decimal total = 0;
            foreach (DataRow row in dt.Rows)
            {
                total += Convert.ToDecimal(row["Subtotal"]);
            }
            lblTotal.Text = "Total: ৳ " + total.ToString("N2");
        }

        private void dgvCart_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                numQuantity.Value = Convert.ToInt32(dgvCart.SelectedRows[0].Cells["Quantity"].Value);
            }
        }

        private void btnUpdateQty_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                int cartId = Convert.ToInt32(dgvCart.SelectedRows[0].Cells["CartID"].Value);
                int qty = (int)numQuantity.Value;
                DataAccess da = new DataAccess();
                da.ExecuteDMLQuery("UPDATE Cart SET Quantity = " + qty + " WHERE CartID = " + cartId);
                LoadCart();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                int cartId = Convert.ToInt32(dgvCart.SelectedRows[0].Cells["CartID"].Value);
                DataAccess da = new DataAccess();
                da.ExecuteDMLQuery("DELETE FROM Cart WHERE CartID = " + cartId);
                LoadCart();
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count > 0)
            {
                new frmCheckout(customerId).ShowDialog();
                LoadCart();
            }
            else
            {
                MessageBox.Show("Cart is empty!");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
