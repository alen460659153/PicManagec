using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
namespace PicManage
{
   
    class DatapagerManager
    {
        DataPagerService objpagerservice = new DataPagerService();
        int recordcount;
        public int RecordCount 
        {  get
            {
                return recordcount;
           }
            set
            {
                
                recordcount = value;
            }        
        }
        int pagesize;
        public int PageSize
        {
            get
            {
                return pagesize;
            }
            set
            {

                pagesize = value;
            }
        }
        int currentpage;
        public int CurrentPage
        {
            get
            {
                return currentpage;
            }
            set
            {

                currentpage = value;
            }
        }
        int pagecount;
        public int PageCount
        {
            get
            {
                if (recordcount != 0)
                {
                    if (recordcount % pagesize != 0)
                        return recordcount / pagesize + 1;
                    else
                        return recordcount / pagesize;
                }
                else
                {
                    this.currentpage = 1;
                    return 0;
                }
            }
            set
            {

                pagecount = value;
            }
        }

        public DataTable Querylog()
        {
            
            int recordCount = 0;

            DataTable dt = objpagerservice.Querylog(this.PageSize, this.CurrentPage,out recordCount);
            this.RecordCount = recordCount;
            return dt;
        
        
        
        }







    }

}
