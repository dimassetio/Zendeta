using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zendeta
{
    public class DbHelper
    {
        Connection conn = new Connection();
        public int insert(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn.getConnection());
            conn.openConn();
            var res = cmd.ExecuteNonQuery();
            conn.closeConn();
            return res;
        }

        

        public DataTable getTable(string sql)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(sql, conn.getConnection());
            conn.openConn();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.closeConn();
            return dt;
        }
            public DataTable getTableCmd(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            cmd.Connection = conn.getConnection();
            conn.openConn();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.closeConn();
            return dt;
        }

        public int insertCmd(SqlCommand cmd)
        {
            cmd.Connection = conn.getConnection();
            conn.openConn();
            var res = cmd.ExecuteNonQuery();
            conn.closeConn();
            return res;
        }
    }
}
