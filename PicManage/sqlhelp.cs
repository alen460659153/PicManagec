using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;





namespace PicManage
{
    class sqlhelp
    {
        string userconn;
      
    
        public DataSet sqlcx(string sql)
        {
            string sql1 = "select * from t_user1";
            string sql2 = "select * from t_user2";
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection("");
            SqlDataAdapter da = new SqlDataAdapter(sql1+sql2,conn);
            da.Fill(ds);
         //   ds.Tables[0]
            return ds;
        }
        public bool sqlux(string sql)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                string sql1 = "update t_user set userid=1";
                string sql2 = "update t_user set userid=2";
                SqlCommand cmd = new SqlCommand(sql1 + sql2, conn);
                int istnum = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
          
        }
    }
}
