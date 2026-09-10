namespace SmartphoneCoverShop
{
    partial class frmManageOffers
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
            this.lblCouponCode = new System.Windows.Forms.Label();
            this.txtCouponCode = new System.Windows.Forms.TextBox();
            this.lblDiscountValue = new System.Windows.Forms.Label();
            this.txtDiscountValue = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
                        this.cmbFilterStatus = new System.Windows.Forms.ComboBox();
            this.lblFilterStatus = new System.Windows.Forms.Label();
            this.dgvOffers = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOffers)).BeginInit();
            this.SuspendLayout();
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Manage Coupons / Offers";
            
            // lblCouponCode
            this.lblCouponCode.AutoSize = true;
            this.lblCouponCode.Location = new System.Drawing.Point(20, 70);
            this.lblCouponCode.Name = "lblCouponCode";
            this.lblCouponCode.Size = new System.Drawing.Size(75, 13);
            this.lblCouponCode.TabIndex = 3;
            this.lblCouponCode.Text = "Coupon Code:";
            
            // txtCouponCode
            this.txtCouponCode.Location = new System.Drawing.Point(120, 67);
            this.txtCouponCode.Name = "txtCouponCode";
            this.txtCouponCode.Size = new System.Drawing.Size(200, 20);
            this.txtCouponCode.TabIndex = 4;
            
            // lblDiscountValue
            this.lblDiscountValue.AutoSize = true;
            this.lblDiscountValue.Location = new System.Drawing.Point(20, 110);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(95, 13);
            this.lblDiscountValue.TabIndex = 7;
            this.lblDiscountValue.Text = "Discount (%):";
            
            // txtDiscountValue
            this.txtDiscountValue.Location = new System.Drawing.Point(120, 107);
            this.txtDiscountValue.Name = "txtDiscountValue";
            this.txtDiscountValue.Size = new System.Drawing.Size(200, 20);
            this.txtDiscountValue.TabIndex = 8;
            
            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 150);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Status:";
            
            // cmbStatus
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] { "Active", "Inactive" });
            this.cmbStatus.Location = new System.Drawing.Point(120, 147);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(200, 21);
            this.cmbStatus.TabIndex = 14;
            
            // btnSave
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(86)))), ((int)(((byte)(174)))));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 190);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 35);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // btnDelete
            this.btnDelete.BackColor = System.Drawing.Color.LightCoral;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(120, 190);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 35);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            // lblFilterStatus
            this.lblFilterStatus.AutoSize = true;
            this.lblFilterStatus.Location = new System.Drawing.Point(500, 105);
            this.lblFilterStatus.Name = "lblFilterStatus";
            this.lblFilterStatus.Size = new System.Drawing.Size(60, 17);
            this.lblFilterStatus.TabIndex = 21;
            this.lblFilterStatus.Text = "Filter Status:";
            // cmbFilterStatus
            this.cmbFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterStatus.Location = new System.Drawing.Point(590, 100);
            this.cmbFilterStatus.Name = "cmbFilterStatus";
            this.cmbFilterStatus.Size = new System.Drawing.Size(150, 25);
            this.cmbFilterStatus.TabIndex = 22;
            this.cmbFilterStatus.SelectedIndexChanged += new System.EventHandler(this.cmbFilterStatus_SelectedIndexChanged);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            
            // btnClear
            this.btnClear.Location = new System.Drawing.Point(220, 190);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 35);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            
            // btnClose
            this.btnClose.Location = new System.Drawing.Point(680, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 30);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            
            // dgvOffers
            this.dgvOffers.AllowUserToAddRows = false;
            this.dgvOffers.AllowUserToDeleteRows = false;
            this.dgvOffers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOffers.Location = new System.Drawing.Point(340, 67);
            this.dgvOffers.Name = "dgvOffers";
            this.dgvOffers.ReadOnly = true;
            this.dgvOffers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOffers.Size = new System.Drawing.Size(420, 158);
            this.dgvOffers.TabIndex = 19;
            this.dgvOffers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOffers_CellClick);
            
            // frmManageOffers
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(780, 260);
            this.Controls.Add(this.dgvOffers);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClear);
                        this.Controls.Add(this.cmbFilterStatus);
            this.Controls.Add(this.lblFilterStatus);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtDiscountValue);
            this.Controls.Add(this.lblDiscountValue);
            this.Controls.Add(this.txtCouponCode);
            this.Controls.Add(this.lblCouponCode);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmManageOffers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Offers";
            this.Load += new System.EventHandler(this.frmManageOffers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOffers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCouponCode;
        private System.Windows.Forms.TextBox txtCouponCode;
        private System.Windows.Forms.Label lblDiscountValue;
        private System.Windows.Forms.TextBox txtDiscountValue;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
                private System.Windows.Forms.ComboBox cmbFilterStatus;
        private System.Windows.Forms.Label lblFilterStatus;
        private System.Windows.Forms.DataGridView dgvOffers;
    }
}

