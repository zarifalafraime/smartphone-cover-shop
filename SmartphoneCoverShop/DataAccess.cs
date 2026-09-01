using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SmartphoneCoverShop
{
    internal class DataAccess : IDisposable
    {
        private SqlConnection sqlcon;
        public SqlConnection Sqlcon
        {
            get { return this.sqlcon; }
            set { this.sqlcon = value; }
        }

        private SqlCommand sqlcom;
        public SqlCommand Sqlcom
        {
            get { return this.sqlcom; }
            set { this.sqlcom = value; }
        }

        private SqlDataAdapter sda;
        public SqlDataAdapter Sda
        {
            get { return this.sda; }
            set { this.sda = value; }
        }

        private DataSet ds;
        public DataSet Ds
        {
            get { return this.ds; }
            set { this.ds = value; }
        }

        public DataAccess()
        {
            // Use the connection string pattern from the project
            this.Sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            Sqlcon.Open();
        }

        private void QueryText(string query)
        {
            this.Sqlcom = new SqlCommand(query, this.Sqlcon);
        }

        public DataSet ExecuteQuery(string sql)
        {
            this.QueryText(sql);
            this.Sda = new SqlDataAdapter(this.Sqlcom);
            this.Ds = new DataSet();
            this.Sda.Fill(this.Ds);
            return Ds;
        }

        public DataTable ExecuteQueryTable(string sql)
        {
            this.QueryText(sql);
            this.Sda = new SqlDataAdapter(this.Sqlcom);
            this.Ds = new DataSet();
            this.Sda.Fill(this.Ds);
            return Ds.Tables[0];
        }

        public int ExecuteDMLQuery(string sql)
        {
            this.QueryText(sql);
            int u = this.Sqlcom.ExecuteNonQuery();
            return u;
        }
        
        public void Dispose()
        {
            if(this.Sqlcon != null && this.Sqlcon.State == ConnectionState.Open)
            {
                this.Sqlcon.Close();
            }
            if (this.Sqlcon != null) this.Sqlcon.Dispose();
            if (this.Sqlcom != null) this.Sqlcom.Dispose();
            if (this.Sda != null) this.Sda.Dispose();
            if (this.Ds != null) this.Ds.Dispose();
        }
    }
}
