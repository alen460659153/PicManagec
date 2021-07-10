using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace PicManage
{
  public  class DataPagerService
    {
             
        
        
        public DataTable Querylog(int pagesize, int currentpage, out int recordcCount)
        {
            SqlParameter[] param = new SqlParameter[]
            {
              new SqlParameter("@PageSize",pagesize),
               new SqlParameter("@CurrentPage",currentpage)        
            
            };
            DataSet ds = MyMeans.GetDataSetByProcedure("Paper", param);
            recordcCount=Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return ds.Tables[0];

        }
    }
}
