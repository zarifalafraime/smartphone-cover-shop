using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SmartphoneCoverShop
{
    public partial class frmOrderHistory : Form
    {
        private int customerId;
        private Label lblTitle;
        private DataGridView dgvOrders;
        private Button btnClose;
        private ComboBox cmbPaymentMethod;
        private Label lblPaymentMethod;

        public frmOrderHistory(int customerId)
        {
            this.customerId = customerId;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.dgvOrders = new DataGridView();
            this.btnClose = new Button();
            this.cmbPaymentMethod = new ComboBox();
            this.lblPaymentMethod = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.SuspendLayout();
            
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Text = "Order History";

            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new Point(480, 25);
            this.lblPaymentMethod.Text = "Filter by Payment:";
            this.lblPaymentMethod.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            this.cmbPaymentMethod.Location = new Point(600, 22);
            this.cmbPaymentMethod.Size = new Size(150, 25);
            this.cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.Items.AddRange(new object[] { "All", "Card", "Cash on Delivery", "DummyCard" });
            this.cmbPaymentMethod.SelectedIndex = 0;
            this.cmbPaymentMethod.SelectedIndexChanged += new EventHandler(this.cmbPaymentMethod_SelectedIndexChanged);
            
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new Point(25, 70);
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new Size(735, 300);
            this.dgvOrders.BackgroundColor = Color.White;
            this.dgvOrders.BorderStyle = BorderStyle.None;
            this.dgvOrders.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvOrders.DefaultCellStyle.SelectionBackColor = Color.FromArgb(116, 86, 174);
            this.dgvOrders.DefaultCellStyle.SelectionForeColor = Color.White;
            this.dgvOrders.DefaultCellStyle.Padding = new Padding(5);
            this.dgvOrders.RowTemplate.Height = 40;
            this.dgvOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252);
            this.dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            this.dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.dgvOrders.EnableHeadersVisualStyles = false;
            this.dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.btnClose.BackColor = Color.White;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnClose.ForeColor = Color.FromArgb(116, 86, 174);
            this.btnClose.Location = new Point(340, 390);
            this.btnClose.Size = new Size(120, 35);
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(784, 451);
            this.Controls.Add(this.cmbPaymentMethod);
            this.Controls.Add(this.lblPaymentMethod);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Order History";
            this.Load += new EventHandler(this.frmOrderHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void frmOrderHistory_Load(object sender, EventArgs e)
        {
            LoadOrders("All");
        }

        private void LoadOrders(string paymentFilter)
        {
            DataAccess da = new DataAccess();
            // Seed a dummy order if none exists for this dummy requirement
            DataTable checkDt = da.ExecuteQueryTable("SELECT OrderID FROM Orders WHERE CustomerID = " + customerId);
            if (checkDt.Rows.Count == 0)
            {
                da.ExecuteDMLQuery("INSERT INTO Orders (CustomerID, OrderDate, TotalAmount, PaymentMethod) VALUES (" + customerId + ", GETDATE(), 599.99, 'DummyCard')");
            }
            
            string query = "SELECT OrderID, OrderDate, TotalAmount, PaymentMethod FROM Orders WHERE CustomerID = " + customerId;
            if (paymentFilter != "All")
            {
                query += " AND PaymentMethod = '" + paymentFilter.Replace("'", "''") + "'";
            }
            query += " ORDER BY OrderDate DESC";

            DataTable dt = da.ExecuteQueryTable(query);
            dgvOrders.DataSource = dt;
        }

        private void cmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaymentMethod.SelectedItem != null)
            {
                LoadOrders(cmbPaymentMethod.SelectedItem.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
