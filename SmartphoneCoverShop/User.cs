using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartphoneCoverShop
{
    class User
    {
        private static string myConn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string Phone { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }

        private const string SelectQuery = "Select * from Users";
        private const string InsertQuery = "Insert Into Users(FullName, Email, Password, UserType, Phone, Status) Values (@FullName, @Email, @Password, @UserType, @Phone, @Status)";
        private const string UpdateQuery = "Update Users set FullName=@FullName, Email=@Email, UserType=@UserType, Phone=@Phone, Status=@Status where UserID=@UserID";
        private const string DeleteQuery = "Delete from Users where UserID=@UserID";

        public DataTable GetUsers()
        {
            var datatable = new DataTable();
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using(SqlCommand com = new SqlCommand(SelectQuery, con))
                {
                    using(SqlDataAdapter adapter = new SqlDataAdapter(com))
                    {
                        adapter.Fill(datatable);
                    }
                }
            }
            return datatable;
        }

        public bool InsertUser(User user)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(InsertQuery, con))
                {
                    com.Parameters.AddWithValue("@FullName", user.FullName);
                    com.Parameters.AddWithValue("@Email", user.Email);
                    com.Parameters.AddWithValue("@Password", user.Password);
                    com.Parameters.AddWithValue("@UserType", user.UserType);
                    com.Parameters.AddWithValue("@Phone", (object)user.Phone ?? DBNull.Value);
                    com.Parameters.AddWithValue("@Status", user.Status);
                    rows = com.ExecuteNonQuery();
                }
            }
            return (rows > 0);
        }

        public bool UpdateUser(User user)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(UpdateQuery, con))
                {                  
                    com.Parameters.AddWithValue("@FullName", user.FullName);
                    com.Parameters.AddWithValue("@Email", user.Email);
                    com.Parameters.AddWithValue("@UserType", user.UserType);
                    com.Parameters.AddWithValue("@Phone", (object)user.Phone ?? DBNull.Value);
                    com.Parameters.AddWithValue("@Status", user.Status);
                    com.Parameters.AddWithValue("@UserID", user.UserID);
                    rows = com.ExecuteNonQuery();
                }
            }
            return (rows > 0);
        }

        public bool DeleteUser(User user)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myConn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(DeleteQuery, con))
                {
                    com.Parameters.AddWithValue("@UserID", user.UserID);
                    rows = com.ExecuteNonQuery();
                }
            }
            return (rows > 0);
        }
    }
}
