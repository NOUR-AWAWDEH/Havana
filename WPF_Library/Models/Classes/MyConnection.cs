using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Classes
{
    public class MyConnection
    {
        string connStr;
        SqlConnection conn = null;

        public MyConnection(string connStr)
        {
            this.connStr = connStr;

        }

        public SqlConnection GetConnection()
        {
            return conn;
        }

        public bool isConnected()
        {
            return conn != null;
        }

        public SqlConnection getConn()
        {
            return conn;
        }
        public Exception connect()
        {
            if (conn == null)
            {
                try
                {
                    conn = new SqlConnection(connStr);
                    conn.Open();
                }
                catch (Exception ex)
                {
                    conn = null;
                    return ex;
                }
            }


            return null;
        }

        public void disconnect()
        {
            if (conn != null)
            {
                conn.Close();
                conn = null;
            }
        }
    }
}

