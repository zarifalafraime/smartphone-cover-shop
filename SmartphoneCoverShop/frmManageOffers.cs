using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SmartphoneCoverShop
{
    public partial class frmManageOffers : Form
    {
        private int shopId;
        private int selectedOfferId = 0;

        public frmManageOffers(int shopId)
        {
            InitializeComponent();
            this.shopId = shopId;
        }

                private void frmManageOffers_Load(object sender, EventArgs e)
        {
            cmbFilterStatus.Items.Add("All");
            cmbFilterStatus.Items.Add("Active");
            cmbFilterStatus.Items.Add("Inactive");
            cmbFilterStatus.SelectedIndex = 0;

            LoadOffers();
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadOffers()
        {
            using (DataAccess da = new DataAccess())
            {
                string query = @"
                    SELECT 
                        OfferID, 
                        OfferName as CouponCode, 
                        DiscountValue as 'Discount (%)', 
                        Status
                    FROM Offers
                    WHERE ShopID = " + shopId;
                dgvOffers.DataSource = da.ExecuteQueryTable(query);
            }
        }

                private void cmbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOffers();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCouponCode.Text))
            {
                MessageBox.Show("Please enter a Coupon Code.");
                return;
            }
            if (!decimal.TryParse(txtDiscountValue.Text, out decimal discountValue) || discountValue <= 0 || discountValue > 100)
            {
                MessageBox.Show("Please enter a valid percentage between 1 and 100.");
                return;
            }

            string couponCode = txtCouponCode.Text.Trim();
            string status = cmbStatus.SelectedItem.ToString();

            using (DataAccess da = new DataAccess())
            {
                if (selectedOfferId == 0)
                {
                    string insertQuery = @"INSERT INTO Offers (ShopID, OfferName, DiscountValue, Status) 
                                           VALUES (@ShopID, @OfferName, @DiscountValue, @Status)";
                    da.Sqlcom = new SqlCommand(insertQuery, da.Sqlcon);
                }
                else
                {
                    string updateQuery = @"UPDATE Offers SET 
                                            ShopID = @ShopID, 
                                            OfferName = @OfferName, 
                                            DiscountValue = @DiscountValue, 
                                            Status = @Status 
                                           WHERE OfferID = @OfferID AND ShopID = @ShopID";
                    da.Sqlcom = new SqlCommand(updateQuery, da.Sqlcon);
                    da.Sqlcom.Parameters.AddWithValue("@OfferID", selectedOfferId);
                }

                da.Sqlcom.Parameters.AddWithValue("@ShopID", shopId);
                da.Sqlcom.Parameters.AddWithValue("@OfferName", couponCode);
                da.Sqlcom.Parameters.AddWithValue("@DiscountValue", discountValue);
                da.Sqlcom.Parameters.AddWithValue("@Status", status);

                da.Sqlcom.ExecuteNonQuery();
                MessageBox.Show("Coupon saved successfully!");
                ClearFields();
                LoadOffers();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedOfferId == 0)
            {
                MessageBox.Show("Please select a coupon to delete.");
                return;
            }

            var confirmResult = MessageBox.Show("Are you sure you want to delete this coupon?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                using (DataAccess da = new DataAccess())
                {
                    string query = "DELETE FROM Offers WHERE OfferID = " + selectedOfferId + " AND ShopID = " + shopId;
                    da.ExecuteDMLQuery(query);
                    MessageBox.Show("Coupon deleted successfully!");
                    ClearFields();
                    LoadOffers();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            selectedOfferId = 0;
            txtCouponCode.Clear();
            txtDiscountValue.Clear();
            cmbStatus.SelectedIndex = 0;
        }

        private void dgvOffers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvOffers.Rows[e.RowIndex];
                selectedOfferId = Convert.ToInt32(row.Cells["OfferID"].Value);
                
                txtCouponCode.Text = row.Cells["CouponCode"].Value.ToString();
                txtDiscountValue.Text = row.Cells["Discount (%)"].Value.ToString();
                cmbStatus.SelectedItem = row.Cells["Status"].Value.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}



