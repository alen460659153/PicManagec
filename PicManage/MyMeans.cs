using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PicManage
{ 
    class MyMeans
    {
        #region  全局变量
        public static string Login_ID = ""; //定义全局变量，记录当前登录的用户编号
        public static string Login_Name = "";  //定义全局变量，记录当前登录的用户名
        public static string Mean_SQL = "", Mean_Table = "", Mean_Field = "";  //定义全局变量，记录“基础信息”各窗体中的表名及SQL语句
        public static SqlConnection My_con;  //定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功
        public static string M_str_sqlcon = "server=192.168.1.200;database=picturemanage;uid=wyd;pwd=ilovejinqiu;pooling=true; max pool size=10;min pool size=5";
        public static string Erpsqlcon = "server=192.168.1.200;database=AIS20140221111803;uid=wyd;pwd=ilovejinqiu;pooling=true; max pool size=10;min pool size=5";
        public static string Login_edit ;
        //public static string M_str_sqlcon = @"Data Source=LVSHUANG0927\\HCDY;Initial Catalog=db_PWMS;Integrated Security=True";

        public static int Login_n = 0;  //用户登录与重新登录的标识
        public static string AllSql = "select * from dbo.CzrBaseTbl";    //存储职工基本信息表中的SQL语句
        //public static int res = 0;
        #endregion
        
        #region  建立数据库连接
        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回SqlConnection对象</returns>
       
        
        public static SqlConnection getcon()
        {
            My_con = new SqlConnection(M_str_sqlcon);   //用SqlConnection对象与指定的数据库相连接
            My_con.Open();  //打开数据库连接
            return My_con;  //返回SqlConnection对象的信息
        }
        #endregion

        #region  测试数据库是否赋加
        /// <summary>
        /// 测试数据库是否赋加
        /// </summary>
        public void con_open()
        {
            getcon();
            //con_close();
        }
        #endregion
        #region  关闭数据库连接
        /// <summary>
        /// 关闭于数据库的连接.
        /// </summary>
        public void con_close()
        {
            if (My_con.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                My_con.Close();   //关闭数据库的连接
                My_con.Dispose();   //释放My_con变量的所有空间
            }
        }
        #endregion
        #region  读取指定表中的信息
        /// <summary>
        /// 读取指定表中的信息.
        /// </summary>
        /// <param name="SQLstr">SQL语句</param>
        /// <returns>返回bool型</returns>
        public SqlDataReader getcom(string SQLstr)
        {
            getcon();   //打开与数据库的连接
            SqlCommand My_com = My_con.CreateCommand(); //创建一个SqlCommand对象，用于执行SQL语句
            My_com.CommandText = SQLstr;    //获取指定的SQL语句
            SqlDataReader My_read = My_com.ExecuteReader(); //执行SQL语名句，生成一个SqlDataReader对象
            return My_read;
        }
        #endregion
        #region 执行SqlCommand命令
        /// <summary>
        /// 执行SqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        public void getsqlcom(string SQLstr)
        {
            getcon();   //打开与数据库的连接
            SqlCommand SQLcom = new SqlCommand(SQLstr, My_con); //创建一个SqlCommand对象，用于执行SQL语句
            SQLcom.ExecuteNonQuery();   //执行SQL语句
            SQLcom.Dispose();   //释放所有空间
            con_close();    //调用con_close()方法，关闭与数据库的连接
        }
        #endregion
        #region 执行保存命令
        /// <summary>
        /// 执行SqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        public void getsqlcomsave(string SQLstr)
        {
            getcon();   //打开与数据库的连接
            SQLstr = "";
            SqlCommand SQLcom = new SqlCommand(SQLstr, My_con); //创建一个SqlCommand对象，用于执行SQL语句
            SQLcom.ExecuteNonQuery();   //执行SQL语句
            SQLcom.Dispose();   //释放所有空间
            con_close();    //调用con_close()方法，关闭与数据库的连接
        }
        #endregion
        #region  创建DataSet对象
        /// <summary>
        /// 创建一个DataSet对象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <param name="M_str_table">表名</param>
        /// <returns>返回DataSet对象</returns>
        public DataSet getDataSet(string SQLstr, string tableName)
        {
            getcon();   //打开与数据库的连接
            SqlDataAdapter SQLda = new SqlDataAdapter(SQLstr, My_con);  //创建一个SqlDataAdapter对象，并获取指定数据表的信息
            DataSet My_DataSet = new DataSet(); //创建DataSet对象
            SQLda.Fill(My_DataSet, tableName);  //通过SqlDataAdapter对象的Fill()方法，将数据表信息添加到DataSet对象中
            con_close();    //关闭数据库的连接
            return My_DataSet;  //返回DataSet对象的信息

            //WritePrivateProfileString(string section, string key, string val, string filePath);
        }
        #endregion
       /// <summary>
       /// 与erp数据库连接获得数据集
       /// </summary>
       /// <param name="sql"></param>
       /// <param name="tableName"></param>
       /// <returns></returns>
        public DataSet getERPDataSet(string sql, string tableName)
        {
            SqlConnection conn = new SqlConnection(Erpsqlcon);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();  //打开与数据库的连接
            SqlDataAdapter SQLda = new SqlDataAdapter(sql, conn);  //创建一个SqlDataAdapter对象，并获取指定数据表的信息
            DataSet My_DataSet = new DataSet(); //创建DataSet对象
            SQLda.Fill(My_DataSet, tableName);  //通过SqlDataAdapter对象的Fill()方法，将数据表信息添加到DataSet对象中
            conn.Close();    //关闭数据库的连接
            return My_DataSet;  //返回DataSet对象的信息

            //WritePrivateProfileString(string section, string key, string val, string filePath);
        }




        #region 获取单一结果查询
        /// <summary>
        /// 获取单一结果查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            SqlConnection conn = new SqlConnection(M_str_sqlcon);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            object result = cmd.ExecuteReader();
            conn.Close();
            return result;
        }
        #endregion
        #region 更新数据操作
        /// <summary>
        /// 更新数据操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Update(string sql)
        {
            SqlConnection conn = new SqlConnection(M_str_sqlcon);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }
        #endregion

        #region
                /// <summary>
       /// 返回一个结果集
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
      {
        SqlConnection conn=new SqlConnection (M_str_sqlcon);
        SqlCommand cmd=new SqlCommand (sql,conn);
        conn.Open();
        SqlDataReader objReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return objReader;
      }
        #endregion
        #region
        /// <summary>
        /// 调用带参数的存储过程
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int UpdateByProcedure(string storeProcedureName,SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(M_str_sqlcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storeProcedureName;            
            conn.Open();
            cmd.Parameters.AddRange(param);
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }
        #endregion
        #region 调用存贮过程获得数据集
        /// <summary>
        /// 调用存贮过程获得数据集
        /// </summary>
        /// <param name="storeprocedurename"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataSet GetDataSetByProcedure(string storeprocedurename, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(M_str_sqlcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storeprocedurename;   
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            conn.Open();
            cmd.Parameters.AddRange(param);
            da.Fill(ds);
            conn.Close();
            return ds;


        }
        #endregion

        #region 调用ERP存贮过程获得数据集
        /// <summary>
        /// 调用存贮过程获得数据集
        /// </summary>
        /// <param name="storeprocedurename"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataSet GetDataSetByERPProcedure(string storeprocedurename, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(Erpsqlcon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storeprocedurename;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            conn.Open();
            cmd.Parameters.AddRange(param);
            da.Fill(ds);
            conn.Close();
            return ds;


        }
        #endregion




    }
}
