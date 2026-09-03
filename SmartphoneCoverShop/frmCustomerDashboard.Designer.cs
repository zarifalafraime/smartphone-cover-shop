namespace SmartphoneCoverShop
{
    partial class frmCustomerDashboard
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
            this.btnBrowseProducts = new System.Windows.Forms.Button();
            this.btnMyCart = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(288, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Customer Dashboard";
            
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblWelcome.Location = new System.Drawing.Point(35, 75);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(115, 21);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Welcome, User";
            
            // 
            // btnBrowseProducts
            // 
            this.btnBrowseProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnBrowseProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseProducts.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBrowseProducts.ForeColor = System.Drawing.Color.White;
            this.btnBrowseProducts.Location = new System.Drawing.Point(40, 130);
            this.btnBrowseProducts.Name = "btnBrowseProducts";
            this.btnBrowseProducts.Size = new System.Drawing.Size(200, 60);
            this.btnBrowseProducts.TabIndex = 2;
            this.btnBrowseProducts.Text = "Browse Products";
            this.btnBrowseProducts.UseVisualStyleBackColor = false;
            this.btnBrowseProducts.Click += new System.EventHandler(this.btnBrowseProducts_Click);
            
            // 
            // btnMyCart
            // 
            this.btnMyCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnMyCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMyCart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMyCart.ForeColor = System.Drawing.Color.White;
            this.btnMyCart.Location = new System.Drawing.Point(260, 130);
            this.btnMyCart.Name = "btnMyCart";
            this.btnMyCart.Size = new System.Drawing.Size(150, 60);
            this.btnMyCart.TabIndex = 3;
            this.btnMyCart.Text = "My Cart";
            this.btnMyCart.UseVisualStyleBackColor = false;
            this.btnMyCart.Click += new System.EventHandler(this.btnMyCart_Click);
            
            // 
            // btnOrderHistory
            // 
            this.btnOrderHistory = new System.Windows.Forms.Button();
            this.btnOrderHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnOrderHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrderHistory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnOrderHistory.ForeColor = System.Drawing.Color.White;
            this.btnOrderHistory.Location = new System.Drawing.Point(430, 130);
            this.btnOrderHistory.Name = "btnOrderHistory";
            this.btnOrderHistory.Size = new System.Drawing.Size(150, 60);
            this.btnOrderHistory.TabIndex = 5;
            this.btnOrderHistory.Text = "Order History";
            this.btnOrderHistory.UseVisualStyleBackColor = false;
            this.btnOrderHistory.Click += new System.EventHandler(this.btnOrderHistory_Click);
            
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.White;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnLogout.Location = new System.Drawing.Point(40, 220);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(100, 35);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Log Out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            
            // 
            // frmCustomerDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnOrderHistory);
            this.Controls.Add(this.btnMyCart);
            this.Controls.Add(this.btnBrowseProducts);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmCustomerDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Dashboard";
            this.Load += new System.EventHandler(this.frmDashboard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnBrowseProducts;
        private System.Windows.Forms.Button btnMyCart;
        private System.Windows.Forms.Button btnOrderHistory;
        private System.Windows.Forms.Button btnLogout;
    }
}
