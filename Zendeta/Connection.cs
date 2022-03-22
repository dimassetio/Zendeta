using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zendeta
{
    public class Connection
    {
        public SqlConnection cnn;

        public Connection()
        {
            string connString = @"Data Source=TEAMOS-PC;Initial Catalog=zendeta;Integrated Security=True";
            cnn = new SqlConnection(connString);
        }

        public SqlConnection getConnection() { return cnn; }

        public void openConn()
        {
            if (cnn.State == System.Data.ConnectionState.Closed)
            {
                cnn.Open();
            }
        }

        public void closeConn()
        {
            if (cnn.State == System.Data.ConnectionState.Open)
            {
                cnn.Close();
            }
        }
    }
}
