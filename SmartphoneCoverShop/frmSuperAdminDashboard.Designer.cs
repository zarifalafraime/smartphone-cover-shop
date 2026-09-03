namespace SmartphoneCoverShop
{
    partial class frmSuperAdminDashboard
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            
            this.btnManageShopOwners = new System.Windows.Forms.Button();
            this.btnManageCategories = new System.Windows.Forms.Button();
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.btnManageReviews = new System.Windows.Forms.Button();
            
            this.pnlStats = new System.Windows.Forms.Panel();
            this.lblTotalSalesTitle = new System.Windows.Forms.Label();
            this.lblTotalSalesValue = new System.Windows.Forms.Label();
            this.lblTotalCommissionTitle = new System.Windows.Forms.Label();
            this.lblTotalCommissionValue = new System.Windows.Forms.Label();
            
            this.lblShopRatings = new System.Windows.Forms.Label();
            this.dgvShopRatings = new System.Windows.Forms.DataGridView();
            
            this.pnlTop.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShopRatings)).BeginInit();
            this.SuspendLayout();
            
            // pnlTop
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.pnlTop.Controls.Add(this.btnClose);
            this.pnlTop.Controls.Add(this.lblWelcome);
            this.pnlTop.Controls.Add(this.btnLogout);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Size = new System.Drawing.Size(900, 60);
            
            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Text = "Smartphone Cover Shop";
            
            // lblWelcome
            this.lblWelcome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular);
            this.lblWelcome.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblWelcome.Location = new System.Drawing.Point(520, 21);
            this.lblWelcome.Text = "Welcome, User";
            
            // btnLogout
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.White;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnLogout.Location = new System.Drawing.Point(760, 14);
            this.btnLogout.Size = new System.Drawing.Size(85, 32);
            this.btnLogout.Text = "LOGOUT";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            
            // btnClose
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSize = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(865, 18);
            this.btnClose.Text = "✕";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            
            // pnlContent
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.pnlContent.Controls.Add(this.btnManageShopOwners);
            this.pnlContent.Controls.Add(this.btnManageCategories);
            this.pnlContent.Controls.Add(this.btnManageUsers);
            this.pnlContent.Controls.Add(this.btnManageReviews);
            this.pnlContent.Controls.Add(this.pnlStats);
            this.pnlContent.Controls.Add(this.lblShopRatings);
            this.pnlContent.Controls.Add(this.dgvShopRatings);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 60);
            this.pnlContent.Size = new System.Drawing.Size(900, 500);
            
            // btnManageShopOwners
            this.btnManageShopOwners.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnManageShopOwners.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageShopOwners.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageShopOwners.Font = new System.Drawing.Font("Nirmala UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnManageShopOwners.ForeColor = System.Drawing.Color.White;
            this.btnManageShopOwners.Location = new System.Drawing.Point(20, 20);
            this.btnManageShopOwners.Size = new System.Drawing.Size(200, 50);
            this.btnManageShopOwners.Text = "Manage Shop Owners";
            this.btnManageShopOwners.Click += new System.EventHandler(this.btnManageShopOwners_Click);
            
            // btnManageCategories
            this.btnManageCategories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnManageCategories.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageCategories.Font = new System.Drawing.Font("Nirmala UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnManageCategories.ForeColor = System.Drawing.Color.White;
            this.btnManageCategories.Location = new System.Drawing.Point(235, 20);
            this.btnManageCategories.Size = new System.Drawing.Size(200, 50);
            this.btnManageCategories.Text = "Manage Categories";
            this.btnManageCategories.Click += new System.EventHandler(this.btnManageCategories_Click);
            
            // btnManageUsers
            this.btnManageUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnManageUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageUsers.Font = new System.Drawing.Font("Nirmala UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnManageUsers.ForeColor = System.Drawing.Color.White;
            this.btnManageUsers.Location = new System.Drawing.Point(450, 20);
            this.btnManageUsers.Size = new System.Drawing.Size(200, 50);
            this.btnManageUsers.Text = "Manage Users";
            this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);

            // btnManageReviews
            this.btnManageReviews.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnManageReviews.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageReviews.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageReviews.Font = new System.Drawing.Font("Nirmala UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnManageReviews.ForeColor = System.Drawing.Color.White;
            this.btnManageReviews.Location = new System.Drawing.Point(665, 20);
            this.btnManageReviews.Size = new System.Drawing.Size(200, 50);
            this.btnManageReviews.Text = "Manage Reviews";
            this.btnManageReviews.Click += new System.EventHandler(this.btnManageReviews_Click);
            
            // pnlStats
            this.pnlStats.BackColor = System.Drawing.Color.White;
            this.pnlStats.Controls.Add(this.lblTotalSalesTitle);
            this.pnlStats.Controls.Add(this.lblTotalSalesValue);
            this.pnlStats.Controls.Add(this.lblTotalCommissionTitle);
            this.pnlStats.Controls.Add(this.lblTotalCommissionValue);
            this.pnlStats.Location = new System.Drawing.Point(20, 90);
            this.pnlStats.Size = new System.Drawing.Size(845, 100);
            this.pnlStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // lblTotalSalesTitle
            this.lblTotalSalesTitle.AutoSize = true;
            this.lblTotalSalesTitle.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular);
            this.lblTotalSalesTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTotalSalesTitle.Text = "Platform Total Sales (All Shops)";
            
            // lblTotalSalesValue
            this.lblTotalSalesValue.AutoSize = true;
            this.lblTotalSalesValue.Font = new System.Drawing.Font("Nirmala UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalSalesValue.ForeColor = System.Drawing.Color.Green;
            this.lblTotalSalesValue.Location = new System.Drawing.Point(20, 50);
            this.lblTotalSalesValue.Text = "$0.00";

            // lblTotalCommissionTitle
            this.lblTotalCommissionTitle.AutoSize = true;
            this.lblTotalCommissionTitle.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular);
            this.lblTotalCommissionTitle.Location = new System.Drawing.Point(400, 20);
            this.lblTotalCommissionTitle.Text = "Total Platform Commission (10%)";
            
            // lblTotalCommissionValue
            this.lblTotalCommissionValue.AutoSize = true;
            this.lblTotalCommissionValue.Font = new System.Drawing.Font("Nirmala UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalCommissionValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.lblTotalCommissionValue.Location = new System.Drawing.Point(400, 50);
            this.lblTotalCommissionValue.Text = "$0.00";

            // lblShopRatings
            this.lblShopRatings.AutoSize = true;
            this.lblShopRatings.Font = new System.Drawing.Font("Nirmala UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblShopRatings.Location = new System.Drawing.Point(20, 210);
            this.lblShopRatings.Text = "All Shop Ratings";

            // dgvShopRatings
            this.dgvShopRatings.Location = new System.Drawing.Point(20, 245);
            this.dgvShopRatings.Size = new System.Drawing.Size(845, 235);
            this.dgvShopRatings.ReadOnly = true;
            this.dgvShopRatings.AllowUserToAddRows = false;
            this.dgvShopRatings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShopRatings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // frmSuperAdminDashboard
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Super Admin Dashboard";
            this.Load += new System.EventHandler(this.frmDashboard_Load);
            
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.pnlStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShopRatings)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label btnClose;
        
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnManageShopOwners;
        private System.Windows.Forms.Button btnManageCategories;
        private System.Windows.Forms.Button btnManageUsers;
        private System.Windows.Forms.Button btnManageReviews;
        
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Label lblTotalSalesTitle;
        private System.Windows.Forms.Label lblTotalSalesValue;
        private System.Windows.Forms.Label lblTotalCommissionTitle;
        private System.Windows.Forms.Label lblTotalCommissionValue;
        
        private System.Windows.Forms.Label lblShopRatings;
        private System.Windows.Forms.DataGridView dgvShopRatings;
    }
}
