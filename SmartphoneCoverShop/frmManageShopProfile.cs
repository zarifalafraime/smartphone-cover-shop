using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace SmartphoneCoverShop
{
    public partial class frmManageShopProfile : Form
    {
        private int _userId;
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

        public frmManageShopProfile(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void frmManageShopProfile_Load(object sender, EventArgs e)
        {
            LoadProfile();
        }

        private void LoadProfile()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ShopName, ShopDescription FROM Shops WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", _userId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtShopName.Text = reader["ShopName"].ToString();
                                txtShopDescription.Text = reader["ShopDescription"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtShopName.Text))
            {
                MessageBox.Show("Shop Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    
                    // Check if shop exists
                    string checkQuery = "SELECT COUNT(*) FROM Shops WHERE UserID = @UserID";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@UserID", _userId);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            // Update
                            string updateQuery = "UPDATE Shops SET ShopName = @ShopName, ShopDescription = @ShopDescription WHERE UserID = @UserID";
                            using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                            {
                                cmd.Parameters.AddWithValue("@ShopName", txtShopName.Text.Trim());
                                cmd.Parameters.AddWithValue("@ShopDescription", txtShopDescription.Text.Trim());
                                cmd.Parameters.AddWithValue("@UserID", _userId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Insert
                            string insertQuery = "INSERT INTO Shops (UserID, ShopName, ShopDescription, Status) VALUES (@UserID, @ShopName, @ShopDescription, 1)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                            {
                                cmd.Parameters.AddWithValue("@ShopName", txtShopName.Text.Trim());
                                cmd.Parameters.AddWithValue("@ShopDescription", txtShopDescription.Text.Trim());
                                cmd.Parameters.AddWithValue("@UserID", _userId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    MessageBox.Show("Shop profile saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
