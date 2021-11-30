using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TechBiteERP.ReportProject.Reports
{
    public class ReportHelper
    {
        //private readonly ApplicationDbContext _db;
        private readonly IConfiguration Configuration;

        public ReportHelper(IConfiguration ConfigurationManager)
        {
            //_db = db;
            Configuration = ConfigurationManager;
            //_connectionString =this.ConfigurationManager.ConnectionStrings["NumbersConnectionString"].ConnectionString;
        }
        public DataTable GetDataTable(string sql)
        {
            string _connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();
            return table;
        }
        public DataTable GetDataTable(string sql, SqlParameter[] parameters)
        {
            string _connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, connection);
            adapter.SelectCommand.Parameters.AddRange(parameters);
            DataTable table = new DataTable(sql);
            adapter.Fill(table);
            connection.Close();
            return table;
        }
        public DataTable GetSPDataTable(string sql, SqlParameter[] parameters)
        {
            string _connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddRange(parameters);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();
            return table;
        }

    }
}
