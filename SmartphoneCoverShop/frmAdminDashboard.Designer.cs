namespace SmartphoneCoverShop
{
    partial class frmAdminDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnManageProfile = new System.Windows.Forms.Button();
            this.btnManageProducts = new System.Windows.Forms.Button();
            this.btnManageCoupons = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblTotalSold = new System.Windows.Forms.Label();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.lblTotalOrders = new System.Windows.Forms.Label();
            this.lblAvgOrderValue = new System.Windows.Forms.Label();
            this.lblInventory = new System.Windows.Forms.Label();
            this.cmbFilterStock = new System.Windows.Forms.ComboBox();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.lblSalesHistory = new System.Windows.Forms.Label();
            this.dgvSalesHistory = new System.Windows.Forms.DataGridView();
            this.lblReviews = new System.Windows.Forms.Label();
            this.dgvReviews = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(249, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Admin Dashboard";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblWelcome.Location = new System.Drawing.Point(35, 65);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(113, 21);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Welcome, User";
            // 
            // btnManageProfile
            // 
            this.btnManageProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnManageProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageProfile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnManageProfile.ForeColor = System.Drawing.Color.White;
            this.btnManageProfile.Location = new System.Drawing.Point(40, 140);
            this.btnManageProfile.Name = "btnManageProfile";
            this.btnManageProfile.Size = new System.Drawing.Size(160, 40);
            this.btnManageProfile.TabIndex = 4;
            this.btnManageProfile.Text = "Shop Profile";
            this.btnManageProfile.UseVisualStyleBackColor = false;
            this.btnManageProfile.Click += new System.EventHandler(this.btnManageProfile_Click);
            // 
            // btnManageProducts
            // 
            this.btnManageProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnManageProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageProducts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnManageProducts.ForeColor = System.Drawing.Color.White;
            this.btnManageProducts.Location = new System.Drawing.Point(210, 140);
            this.btnManageProducts.Name = "btnManageProducts";
            this.btnManageProducts.Size = new System.Drawing.Size(160, 40);
            this.btnManageProducts.TabIndex = 5;
            this.btnManageProducts.Text = "Manage Products";
            this.btnManageProducts.UseVisualStyleBackColor = false;
            this.btnManageProducts.Click += new System.EventHandler(this.btnManageProducts_Click);
            // 
            // btnManageCoupons
            // 
            this.btnManageCoupons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnManageCoupons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageCoupons.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnManageCoupons.ForeColor = System.Drawing.Color.White;
            this.btnManageCoupons.Location = new System.Drawing.Point(380, 140);
            this.btnManageCoupons.Name = "btnManageCoupons";
            this.btnManageCoupons.Size = new System.Drawing.Size(160, 40);
            this.btnManageCoupons.TabIndex = 6;
            this.btnManageCoupons.Text = "Manage Coupons";
            this.btnManageCoupons.UseVisualStyleBackColor = false;
            this.btnManageCoupons.Click += new System.EventHandler(this.btnManageCoupons_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.White;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnLogout.Location = new System.Drawing.Point(900, 20);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(100, 35);
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "Log Out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblTotalSold
            // 
            this.lblTotalSold.AutoSize = true;
            this.lblTotalSold.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalSold.ForeColor = System.Drawing.Color.Green;
            this.lblTotalSold.Location = new System.Drawing.Point(35, 100);
            this.lblTotalSold.Name = "lblTotalSold";
            this.lblTotalSold.Size = new System.Drawing.Size(132, 21);
            this.lblTotalSold.TabIndex = 2;
            this.lblTotalSold.Text = "Products Sold: 0";
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.AutoSize = true;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.Green;
            this.lblTotalRevenue.Location = new System.Drawing.Point(220, 100);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(166, 21);
            this.lblTotalRevenue.TabIndex = 3;
            this.lblTotalRevenue.Text = "Total Revenue: $0.00";
            // 
                        // lblTotalOrders
            this.lblTotalOrders.AutoSize = true;
            this.lblTotalOrders.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalOrders.ForeColor = System.Drawing.Color.Green;
            this.lblTotalOrders.Location = new System.Drawing.Point(420, 100);
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Size = new System.Drawing.Size(120, 21);
            this.lblTotalOrders.TabIndex = 24;
            this.lblTotalOrders.Text = "Total Orders: 0";
            // lblAvgOrderValue
            this.lblAvgOrderValue.AutoSize = true;
            this.lblAvgOrderValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAvgOrderValue.ForeColor = System.Drawing.Color.Green;
            this.lblAvgOrderValue.Location = new System.Drawing.Point(600, 100);
            this.lblAvgOrderValue.Name = "lblAvgOrderValue";
            this.lblAvgOrderValue.Size = new System.Drawing.Size(140, 21);
            this.lblAvgOrderValue.TabIndex = 25;
            this.lblAvgOrderValue.Text = "Avg Order: .00";
            // lblInventory
            // 
            this.lblInventory.AutoSize = true;
            this.lblInventory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblInventory.Location = new System.Drawing.Point(35, 200);
            this.lblInventory.Name = "lblInventory";
            this.lblInventory.Size = new System.Drawing.Size(193, 21);
            this.lblInventory.TabIndex = 8;
            this.lblInventory.Text = "Inventory / Stock Status";
            // 
            // cmbFilterStock
            // 
            this.cmbFilterStock.Location = new System.Drawing.Point(379, 203);
            this.cmbFilterStock.Name = "cmbFilterStock";
            this.cmbFilterStock.Size = new System.Drawing.Size(121, 21);
            this.cmbFilterStock.TabIndex = 14;
            // 
            // dgvInventory
            // 
            this.dgvInventory.AllowUserToAddRows = false;
            this.dgvInventory.AllowUserToDeleteRows = false;
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventory.Location = new System.Drawing.Point(40, 230);
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.Size = new System.Drawing.Size(460, 150);
            this.dgvInventory.TabIndex = 9;
            // 
            // lblSalesHistory
            // 
            this.lblSalesHistory.AutoSize = true;
            this.lblSalesHistory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSalesHistory.Location = new System.Drawing.Point(515, 200);
            this.lblSalesHistory.Name = "lblSalesHistory";
            this.lblSalesHistory.Size = new System.Drawing.Size(109, 21);
            this.lblSalesHistory.TabIndex = 10;
            this.lblSalesHistory.Text = "Sales History";
            // 
            // dgvSalesHistory
            // 
            this.dgvSalesHistory.AllowUserToAddRows = false;
            this.dgvSalesHistory.AllowUserToDeleteRows = false;
            this.dgvSalesHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSalesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesHistory.Location = new System.Drawing.Point(520, 230);
            this.dgvSalesHistory.Name = "dgvSalesHistory";
            this.dgvSalesHistory.ReadOnly = true;
            this.dgvSalesHistory.Size = new System.Drawing.Size(450, 150);
            this.dgvSalesHistory.TabIndex = 11;
            // 
            // lblReviews
            // 
            this.lblReviews.AutoSize = true;
            this.lblReviews.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblReviews.Location = new System.Drawing.Point(35, 395);
            this.lblReviews.Name = "lblReviews";
            this.lblReviews.Size = new System.Drawing.Size(166, 21);
            this.lblReviews.TabIndex = 12;
            this.lblReviews.Text = "Reviews and Ratings";
            // 
            // dgvReviews
            // 
            this.dgvReviews.AllowUserToAddRows = false;
            this.dgvReviews.AllowUserToDeleteRows = false;
            this.dgvReviews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReviews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReviews.Location = new System.Drawing.Point(40, 420);
            this.dgvReviews.Name = "dgvReviews";
            this.dgvReviews.ReadOnly = true;
            this.dgvReviews.Size = new System.Drawing.Size(920, 150);
            this.dgvReviews.TabIndex = 13;
            // 
            // frmAdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvReviews);
            this.Controls.Add(this.lblReviews);
            this.Controls.Add(this.dgvSalesHistory);
            this.Controls.Add(this.lblSalesHistory);
            this.Controls.Add(this.cmbFilterStock);
            this.Controls.Add(this.dgvInventory);
            this.Controls.Add(this.lblInventory);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnManageCoupons);
            this.Controls.Add(this.btnManageProducts);
            this.Controls.Add(this.btnManageProfile);
            this.Controls.Add(this.lblTotalRevenue);
            this.Controls.Add(this.lblTotalOrders);
            this.Controls.Add(this.lblAvgOrderValue);
            this.Controls.Add(this.lblTotalSold);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmAdminDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Dashboard";
            this.Load += new System.EventHandler(this.frmDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblTotalSold;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.Label lblAvgOrderValue;
        private System.Windows.Forms.Button btnManageProfile;
        private System.Windows.Forms.Button btnManageProducts;
        private System.Windows.Forms.Button btnManageCoupons;
        private System.Windows.Forms.Button btnLogout;
        
        private System.Windows.Forms.Label lblInventory;
                private System.Windows.Forms.ComboBox cmbFilterStock;
        private System.Windows.Forms.DataGridView dgvInventory;
        
        private System.Windows.Forms.Label lblSalesHistory;
        private System.Windows.Forms.DataGridView dgvSalesHistory;

        private System.Windows.Forms.Label lblReviews;
        private System.Windows.Forms.DataGridView dgvReviews;
    }
}



