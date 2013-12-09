using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace StuandDor.DAL
{
    public class SqlHelper
    {
	    //数据库连接字符串
            public static string connStr = "Integrated Security=SSPI;Persist Security Info=False;" + @"Initial Catalog=DBStuAndDorInf;Data Source=DAWEI";
        
            public static int ExecuteNonQuery(string sql, params SqlParameter[] pms)
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        if (pms != null)
                        {
                            cmd.Parameters.AddRange(pms);
                        }
                        con.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }


            // 执行sql语句，返回单个值。
         
            public static object ExecuteScalar(string sql, params SqlParameter[] pms)
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        if (pms != null)
                        {
                            cmd.Parameters.AddRange(pms);

                        }
                        con.Open();
                        return cmd.ExecuteScalar();
                    }
                }


            }


         
            // 执行sql语句返回DataReader实例
          
            public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] pms)
            {
                SqlConnection con = new SqlConnection(connStr);
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        if (pms != null)
                        {
                            cmd.Parameters.AddRange(pms);
                        }
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        return reader;

                    }
                }
                catch
                {
                    if (con != null)
                    {
                        con.Close(); con.Dispose();
                    }
                    throw;
                }
            }
           

            // 执行sql返回一个DataTable实例
          
            public static DataTable ExecuteDataTable(string sql, params SqlParameter[] pms)
            {
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, connStr);
                if (pms != null)
                {
                    sqlAdapter.SelectCommand.Parameters.AddRange(pms);
                }
                DataTable dt = new DataTable();
                sqlAdapter.Fill(dt);
                return dt;
            }



            // 测试连接数据库是否成功
           
            public static bool ConnectDB()
            {
                try
                {
                    SqlConnection con = new SqlConnection(connStr);
                    con.Open();
                    con.Close();
                    con.Dispose();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
   
}
