using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.CrossCutting.Data.Context
{
    public class DapperContext
    {
        public SqlConnection Connection { get; set; }

        public DapperContext(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration.GetConnectionString("SqlConnection"));
        }

        public void Open()
        {
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
