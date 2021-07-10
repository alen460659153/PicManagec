using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using MyVideo;

namespace PicManage
{
    public partial class Formxz : Form
    {
        MyMeans add = new MyMeans();
        MyModule MyMC = new MyModule();
        private DatapagerManager objPagerManager = new DatapagerManager();
        private Video objVideo = null;
        public Formxz()
        {
            InitializeComponent();
            toolStripStatusLabel3.Text = DateTime.Now.ToString() + "    ";
           // this.N_First.Click += new System.EventHandler(this.N_First_Click);
           
            label17.Text = MyMeans.Login_Name;
            if (MyMeans.Login_edit == "0")
            {
                button7.Enabled = false;
                button9.Enabled = false;
                button3.Enabled = false;
                button2.Enabled = false;
               

            }

            this.cbbPageSize.SelectedIndex = 0;
            this.dataGridView1.AutoGenerateColumns = false;
            this.btnFirst.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnPrevious.Enabled = false;
            this.btnLast.Enabled = false;
            this.btngoto.Enabled = false;

            this.btnCloseVideo.Enabled = false;
            this.btnTake.Enabled = false;
          // this.label1.ForeColor = Color.White;

          



        }
       
        private void Formxz_Load(object sender, EventArgs e)
        {             
           
            string sql1 = "SELECT * FROM TypeValue ";
            string sql2 = "SELECT * FROM ZZType ";
            string sql3 = "SELECT * FROM RsType ";
            string sql4 = "SELECT * FROM WgType ";
            string sql5 = "SELECT * FROM WLType ";
            string sql6 = "SELECT * FROM YBType ";
            string sql7 = "SELECT * FROM ColorType ";
            string sql8 = "SELECT * FROM MaterialType ";
            string sql9 = "SELECT * FROM WidthType ";
            string sql10 = "SELECT * FROM ZWType ";
            string sql11 = "SELECT * FROM TXType ";
            wyd(sql1, listView1);
            wyd(sql2, listView2);
            wyd(sql3, listView3);
            wyd(sql4, listView4);
            wyd(sql5, listView5);
            wyd(sql6, listView6);
            wyd(sql7, listView7);
            wyd(sql8, listView8);
            wyd(sql9, listView9);
            wyd(sql10, listView10);
            wyd(sql11, listView11);
                     


        }
        /// <summary>
        /// listview 加载选项
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        public void  wyd (string sql,ListView  list)
        {
            //string sqlcon = "server=192.168.1.200;database=picturemanage;uid=wyd;pwd=ilovejinqiu";
            SqlConnection mycom = new SqlConnection(MyMeans.M_str_sqlcon);
            mycom.Open();
             
            SqlCommand com1 = new SqlCommand(sql, mycom);
            SqlDataReader sdr = com1.ExecuteReader();
            while (sdr.Read())
            {

                list.Items.Add(sdr[0].ToString());
            }
            mycom.Close();
        }

        /// <summary>
        /// 选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
             if (ofd.ShowDialog() == DialogResult.OK)
              {
                 this.pictureBox1.Image = Image.FromFile(ofd.FileName);
                 //获取文件路径
                 //string i = System.IO.Path.GetDirectoryName(ofd.FileName);     
               }
            
            
           
        }
        


        /// <summary>
        /// 图片保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //string connStr = "server=192.168.1.200;database=picturemanage;uid=wyd;pwd=ilovejinqiu";
            using (SqlConnection conn = new SqlConnection(MyMeans.M_str_sqlcon))
            {
                int t = wydmax();
                string TypeName = lj(listView1);
                string ZZName = lj(listView2);
                string RsName = lj(listView3);
                string WgName = lj(listView4);
                string YBName = lj(listView5);
                string WLName = lj(listView6);
                string ColorName = lj(listView7);
                string MaterialName = lj(listView8);
                string WidthName = lj(listView9);
                string ZWName = lj(listView10);
                string TxName = lj(listView11);
                DateTime FDate = DateTime.Now;
                string FBillOr = this.label17.Text;
                string Remark = this.textBox3.Text;
                string FileNamet = this.textBox1.Text;
                string pic = "无图片";
                string jbsx = TypeName + ZZName + RsName + WgName + WLName + YBName + ColorName + MaterialName + WidthName + ZWName + TxName;
                if (jbsx == "")
                {
                    MessageBox.Show("至少输入一项基本信息");

                }
                else
                {
                    string sql1 = "insert ImageList(sn, TypeName, ZZName, RsName, WgName, WLName, YBName, ColorName, MaterialName, WidthName, ZWName, TxName, FDate, FBillOr, Remark, FileName )";
                    string sql2 = " values('" + t + "', '" + TypeName + "', '" + ZZName + "', '" + RsName + "', '" + WgName + "', '" + WLName + "', '" + YBName + "', '" + ColorName + "', '" + MaterialName + "', '" + WidthName + "', '" + ZWName + "', '" + TxName + "', '" + FDate + "', '" + FBillOr + "', '" + Remark + "', '" + FileNamet + "')";
                    string sql3 = sql1 + sql2;

                    //MyMeans add = new MyMeans();
                    add.getsqlcom(sql3);
                    if (this.pictureBox1.Image != null)
                    {
                        string sql = "Insert Into ImageData(sn,ImageList) Values (@sn,@ImgData) ";

                        byte[] imageBytes = GetImageBytes(pictureBox1.Image);
                        using (SqlCommand cmd = new SqlCommand(sql))
                        {
                            SqlParameter param = new SqlParameter("ImgData", SqlDbType.VarBinary, imageBytes.Length);
                            SqlParameter sn = new SqlParameter("sn", SqlDbType.Int);
                            param.Value = imageBytes;

                            sn.Value = t;
                            cmd.Parameters.Add(param);
                            cmd.Parameters.Add(sn);
                            cmd.Connection = conn;
                            conn.Open();
                            int i = cmd.ExecuteNonQuery();
                            pic="有图片";
                            //MessageBox.Show(i.ToString() + "条有图片记录已增加！");
                            conn.Close();
                        }
                    }
                    MessageBox.Show("1条" + pic +"记录已增加！");
                    button6_Click(this, e);
                    btnCloseVideo_Click(null, null);
                }
               ;
            }
        }
        /// <summary>
        /// 取最大号
        /// </summary>
        /// <returns>最大号</returns>
        private int wydmax()
        {
            //string sqlcon = "server=192.168.1.200;database=picturemanage;uid=wyd;pwd=ilovejinqiu";
            SqlConnection mycom = new SqlConnection(MyMeans.M_str_sqlcon);
            mycom.Open();
            string sql = "select MAX(sn)+1 from ImageList";
            SqlCommand com1 = new SqlCommand(sql, mycom);
         int   i =  Convert.ToInt32(com1.ExecuteScalar());
         return i;
        }



        /// <summary>
        /// 图片转换二进制
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
            private byte[] GetImageBytes(Image image)
        {
            MemoryStream mstream = new MemoryStream();
          
            image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byteData = new Byte[mstream.Length];
            mstream.Position = 0;
            mstream.Read(byteData, 0, byteData.Length);
            mstream.Close();
            return byteData;
        }
            private static byte[] GetBytes(Image image)
            {
                try
                {
                    if (image == null) return null;
                    using (Bitmap bitmap = new Bitmap(image))
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            return stream.GetBuffer();
                        }
                    }
                }
                finally
                {
                    if (image != null)
                    {
                        //image.Dispose();
                        //image = null;
                    }
                }
            }  



            private void button3_Click(object sender, EventArgs e)
            {
                string ypeValue = lj(listView1);
                string ZZType = lj(listView2);
                string RsType = lj(listView3);
                string WgType = lj(listView4);
                string WLType = lj(listView5);
                string YBType = lj(listView6);
                string ColorType = lj(listView7);
                string MaterialType = lj(listView8);
                string WidthType = lj(listView9);
                string ZWType = lj(listView10);
                string TXType = lj(listView11);
                
              
              
                
                button6_Click(this,e);
              

            }
          /// <summary>
          /// 获取 listview 选择项拼接成字符串
          /// </summary>
          /// <param name="lis"></param>
 
          private string  lj(ListView lis)
            {
                string list = "";
                for (int i = 0; i < lis.CheckedItems.Count; i++)
                {
                    if (lis.CheckedItems[i].Checked)
                    {
                        list += lis.CheckedItems[i].Text + "  ";
                      
                    }
                }
              return list;
            }
       /// <summary>
       /// 清除选项值
       /// </summary>
       /// <param name="lis"></param>
   
        private void clear(ListView lis)
          {
             
              for (int i = 0; i < lis.Items.Count; i++)
              {
                  if (lis.Items[i].Checked)
                  {
                      lis.Items[i].Checked = false;

                  }
              }
            

          }

            
        /// <summary>
            /// dataGridView1重绘边框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            private void dataGridView1_Paint_1(object sender, PaintEventArgs e)
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.ForeColor = Color.Blue;
                foreach (DataGridViewColumn col in this.dataGridView1.Columns)
                {
                    col.HeaderCell.Style = style;
                }
                this.dataGridView1.EnableHeadersVisualStyles = false;
                e.Graphics.DrawRectangle(Pens.CadetBlue, new Rectangle(0, 0, this.dataGridView1.Width - 1, this.dataGridView1.Height - 1));
            }
        /// <summary>
            /// pictureBox1重绘边框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            private void pictureBox1_Paint(object sender, PaintEventArgs e)
            {
                PictureBox p = (PictureBox)sender;
                Pen pp = new Pen(Color.CadetBlue);
                e.Graphics.DrawRectangle(pp, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.X + e.ClipRectangle.Width - 1, e.ClipRectangle.Y + e.ClipRectangle.Height - 1);
            }

           
        /// <summary>
        /// 双击小图显示大图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            private void pictureBox1_DoubleClick(object sender, EventArgs e)
            {
                Form2 bg = new Form2();
                bg.Show();
                bg.pictureBox1.Image = this.pictureBox1.Image;
            }
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            private void button6_Click(object sender, EventArgs e)
            {
                
                ////ListView[] list = new ListView[11];

                //for (int i = 1; i < 12; i++)
                //{
                //    clear(ListView & [i]);
                //}
                clear(listView1);
                clear(listView2);
                clear(listView3);
                clear(listView4);
                clear(listView5);
                clear(listView6);
                clear(listView7);
                clear(listView8);
                clear(listView9);
                clear(listView10);
                clear(listView11);
                textBox1.Clear();
                textBox3.Clear();
                pictureBox1.Image = null;
                
            }

            private void picclear_Click(object sender, EventArgs e)
            {
                pictureBox1.Image = null;
            }
        /// <summary>
        /// 显示所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            private void button4_Click(object sender, EventArgs e)
            {
                //string sql = "select sn , TypeName+ZZName+RsName+WgName+ WLName+YBName+ColorName+MaterialName+WidthName+ZWName+TxName as prop,  FileName,FDate,FBillOr,Remark from dbo.ImageList order by sn";
                //DataSet  MyDS_Grid = add.getDataSet(sql, "tb_Stuffbusic");
                //dataGridView1.DataSource = MyDS_Grid.Tables[0];
                //dataGridView1.AutoGenerateColumns = true;  //是否自动创建列
                //dataGridView1.Columns[0].Width = 35;
                //dataGridView1.Columns[1].Width = 400;
                //dataGridView1.Columns[2].Width = 100;
                //dataGridView1.Columns[3].Width = 70;
                //dataGridView1.Columns[4].Width = 60;
                this.N_Previous.Enabled = true;
                this.N_First.Enabled = true;
                this.N_Next.Enabled = true;
                this.N_Cauda.Enabled = true;


                this.objPagerManager.CurrentPage = 1;
                query();
                this.btnFirst.Enabled = false;
                this.btnPrevious.Enabled = false;


            }
            private void query()
            {
                this.btnFirst.Enabled = true;
                this.btnNext.Enabled = true;
                this.btnPrevious.Enabled = true;
                this.btnLast.Enabled = true;
                this.objPagerManager.PageSize = Convert.ToInt32(this.cbbPageSize.Text);
                DataTable dt=objPagerManager.Querylog();
                this.dataGridView1.DataSource=dt;
                dataGridView1.AutoGenerateColumns = true;  //是否自动创建列
                dataGridView1.Columns[0].Width = 35;
                dataGridView1.Columns[1].Width = 400;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 70;
                dataGridView1.Columns[4].Width = 60;
            
                

                this.lblRecordCount.Text = objPagerManager.RecordCount.ToString();
                this.lblPageCount.Text = objPagerManager.PageCount.ToString();
                if (this.lblPageCount.Text == "0")
                {
                    this.lblCurrentpage.Text = "0";

                }
                else 
                {
                    this.lblCurrentpage.Text = objPagerManager.CurrentPage.ToString();
                }
                if (this.lblCurrentpage.Text == "0" || this.lblPageCount.Text == "1")
                {
                    this.btnFirst.Enabled = false;
                    this.btnNext.Enabled = false;
                    this.btnPrevious.Enabled = false;
                    this.btnLast.Enabled = false;
                    this.btngoto.Enabled = false;
                
                }
                else
                {
                    this.btngoto.Enabled=true;
                
                }
                if(dt.Rows.Count==0)
                {
                  MessageBox.Show("没有找到符合条件的记录！","查询提示");
                }
                
                

            
            }
            


            private void N_First_Click(object sender, EventArgs e)
            {
                int ColInd = 0;
                if (dataGridView1.CurrentCell.ColumnIndex == -1 || dataGridView1.CurrentCell.ColumnIndex > 1)
                    ColInd = 0;
                else
                    ColInd = dataGridView1.CurrentCell.ColumnIndex;
                if ((((Button)sender).Name) == "N_First")
                {
                    dataGridView1.CurrentCell = this.dataGridView1[ColInd, 0];
                    MyMC.Ena_Button(N_First, N_Previous, N_Next, N_Cauda, 0, 0, 1, 1);
                    dataGridView1_CellContentClick(null, null);
                }
                if ((((Button)sender).Name) == "N_Previous")
                {
                    if (dataGridView1.CurrentCell.RowIndex == 0)
                    {
                        MyMC.Ena_Button(N_First, N_Previous, N_Next, N_Cauda, 0, 0, 1, 1);
                        dataGridView1_CellContentClick(null, null);
                    }
                    else
                    {
                        dataGridView1.CurrentCell = this.dataGridView1[ColInd, dataGridView1.CurrentCell.RowIndex - 1];
                        MyMC.Ena_Button(N_First, N_Previous, N_Next, N_Cauda, 1, 1, 1, 1);
                        dataGridView1_CellContentClick(null, null);
                    }
                }
                if ((((Button)sender).Name) == "N_Next")
                {
                    if (dataGridView1.CurrentCell.RowIndex == dataGridView1.RowCount - 2)
                    {
                        MyMC.Ena_Button(N_First, N_Previous, N_Next, N_Cauda, 1, 1, 0, 0);
                        dataGridView1_CellContentClick(null, null);
                    }
                    else
                    {
                        dataGridView1.CurrentCell = this.dataGridView1[ColInd, dataGridView1.CurrentCell.RowIndex + 1];
                        MyMC.Ena_Button(N_First, N_Previous, N_Next, N_Cauda, 1, 1, 1, 1);
                        dataGridView1_CellContentClick(null, null);
                    }
                }
                if ((((Button)sender).Name) == "N_Cauda")
                {
                    dataGridView1.CurrentCell = this.dataGridView1[ColInd, dataGridView1.RowCount - 2];
                    MyMC.Ena_Button(N_First, N_Previous, N_Next, N_Cauda, 1, 1, 0, 0);
                    dataGridView1_CellContentClick(null, null);
                }

            }

            private void N_Previous_Click(object sender, EventArgs e)
            {
                //MessageBox.Show("1");
                N_First_Click(sender, e);
                dataGridView1_CellContentClick(null, null);
            }

            private void N_Next_Click(object sender, EventArgs e)
            {
                N_First_Click(sender, e);
                dataGridView1_CellContentClick(null, null);
            }

            private void N_Cauda_Click(object sender, EventArgs e)
            {
                N_First_Click(sender, e);
                dataGridView1_CellContentClick(null, null);
            }

            private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
            {
                
                //string t = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                //MessageBox.Show(t);
                //fjie(listView1);
                //fjie(listView2);
                //fjie(listView3);
                //fjie(listView4);
                //fjie(listView5);
                //fjie(listView6);
                //fjie(listView7);
                //fjie(listView8);
                //fjie(listView9);
                //fjie(listView10);
                //fjie(listView11);



                

            }

            private void fjie(ListView aa)
            {
                string t = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            
                for (int i = 0; i <aa.Items.Count; i++)
                {
                    string x = aa.Items[i].Text;
                    if (t.Contains(x))
                    {

                        aa.Items[i].Checked = true;
                    }
                    else
                    {
                        aa.Items[i].Checked = false;
                    }
                }
            
            
            
            }

            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                //string t = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                fjie(listView1);
                fjie(listView2);
                fjie(listView3);
                fjie(listView4);
                fjie(listView5);
                fjie(listView6);
                fjie(listView7);
                fjie(listView8);
                fjie(listView9);
                fjie(listView10);
                fjie(listView11);
                textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

                //if (dataGridView1.CurrentRow.Cells[0].Value == null)
                //{
                //    MessageBox.Show("已到末行！");

                //}
                //else
                //{
                int i = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                //}
                string sql = "select ImageList from ImageData where sn='" + i + "'";
                pictureBox1.Image = null;
                ShowImage(sql);
                this.button2.Enabled = false;

                //显示ERP库存

                //string sqlERP = "select v2.FNumber as 编码,v5.fname as 仓库,convert(decimal(18,2),v4.FQty,0) as 数量,v3.FName as 单位,v4.FBatchNo as 批号,v6.FNumber  as 仓址  from  t_ICItem  v2  inner join t_MeasureUnit V3 ON v2.FUnitID=v3.FMeasureUnitID  left join ICInventory v4 on v2.FItemID=v4.FItemID  inner join t_Stock v5 on v4.FStockID=v5.FItemID left join t_StockPlace v6 on v6.FSPID=v4.FStockPlaceID  where v4.FQty>0 and v2.FNumber like'%{0}'  order by v2.FNumber";
                string s = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                s = s.Replace(".jpg", "");
                if (s.Trim() != "" || s.Trim().Length != 0)
                {
                    // sqlERP = string.Format(sqlERP, s);                
                    // DataSet MyDS_Grid = add.getERPDataSet(sqlERP, "kucun");


                    SqlParameter[] param = new SqlParameter[]
            {
              new SqlParameter("@hh",s)                 
            
            };
                    DataSet ds = MyMeans.GetDataSetByERPProcedure("kc", param);
                    dataGridView2.DataSource = ds.Tables[0];

                    //dataGridView2.DataSource = MyDS_Grid.Tables[0];
                    dataGridView2.AutoGenerateColumns = true;  //是否自动创建列
                    dataGridView2.Columns[0].Width = 80;
                    dataGridView2.Columns[1].Width = 80;
                    dataGridView2.Columns[2].Width = 80;
                    dataGridView2.Columns[3].Width = 35;
                    dataGridView2.Columns[4].Width = 80;


                }
            }
            private void ShowImage(string sql)
     {
     //调用方法如：ShowImage("select Photo from UserPhoto where UserNo='" + userno +"'");
     // string sqlcon = "server=192.168.1.200;database=picturemanage;uid=sa;pwd=";
         SqlConnection conn = new SqlConnection(MyMeans.M_str_sqlcon);
            //conn.Open();
     SqlCommand cmd = new SqlCommand(sql, conn);
     conn.Open();
     byte[] b= (byte[])cmd.ExecuteScalar();
     if (b !=null)
     {
     MemoryStream stream = new MemoryStream(b, true);
     stream.Write(b, 0, b.Length);
     //pictureBox1.Image = null;
      pictureBox1.Image = new Bitmap(stream);
      stream.Close();
 

     }
     conn.Close();
     }
            private void LisEdit(DataGridView aa)
            {
                string TypeName = lj(listView1);
                string ZZName = lj(listView2);
                string RsName = lj(listView3);
                string WgName = lj(listView4);
                string YBName = lj(listView5);
                string WLName = lj(listView6);
                string ColorName = lj(listView7);
                string MaterialName = lj(listView8);
                string WidthName = lj(listView9);
                string ZWName = lj(listView10);
                string TxName = lj(listView11);
                DateTime FDate = DateTime.Now;
                string FBillOr = this.label17.Text;                
                string FileNamet = this.textBox1.Text;
                string remark = this.textBox3.Text;
                //string pic = "无图片";
                string jbsx = TypeName + ZZName + RsName + WgName + WLName + YBName + ColorName + MaterialName + WidthName + ZWName + TxName + FileNamet + remark;

                //string a=aa.CurrentRow.Cells[0].Value.ToString() ;
                //string b = aa.CurrentRow.Cells[1].Value.ToString();
                //string c = aa.CurrentRow.Cells[2].Value.ToString();
                //string d = aa.CurrentRow.Cells[3].Value.ToString();
                //string e = aa.CurrentRow.Cells[4].Value.ToString();
                //string f = aa.CurrentRow.Cells[5].Value.ToString();




                aa.CurrentRow.Cells[1].Value = jbsx;
                aa.CurrentRow.Cells[2].Value = FileNamet;
                aa.CurrentRow.Cells[3].Value = FDate;
                aa.CurrentRow.Cells[4].Value = FBillOr;
                aa.CurrentRow.Cells[5].Value = remark;

            }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            private void button5_Click(object sender, EventArgs e)
            {
                this.btnFirst.Enabled = false;
                this.btnNext.Enabled = false;
                this.btnPrevious.Enabled = false;
                this.btnLast.Enabled = false;
                this.btngoto.Enabled = false;
                
                
                string TypeName = lj(listView1);
                string ZZName = lj(listView2);
                string RsName = lj(listView3);
                string WgName = lj(listView4);
                string YBName = lj(listView5);
                string WLName = lj(listView6);
                string ColorName = lj(listView7);
                string MaterialName = lj(listView8);
                string WidthName = lj(listView9);
                string ZWName = lj(listView10);
                string TxName = lj(listView11);
                DateTime FDate = DateTime.Now;
                string FBillOr = this.label17.Text;
                //string Remark = "";
                string FileNamet = this.textBox1.Text;
                string remark = this.textBox3.Text;
                //string pic = "无图片";
                string jbsx = TypeName + ZZName + RsName + WgName + WLName + YBName + ColorName + MaterialName + WidthName + ZWName + TxName + FileNamet + remark;
                if (jbsx == "")
                {
                    MessageBox.Show("至少输入一项查询的基本信息或货号");

                }
                else
                {
                    //string sql = "select t.sn,t.prop, t.FileName,t.FDate,t.FBillOr,t.Remark from (select sn , rtrim(TypeName)+rtrim(ZZName)+rtrim(RsName)+rtrim(WgName)+ rtrim(WLName)+rtrim(YBName)+rtrim(ColorName)+rtrim(MaterialName)+rtrim(WidthName)+rtrim(ZWName)+rtrim(TxName) as prop,  FileName,FDate,FBillOr,Remark from dbo.ImageList)t where t.prop  like'%{0}%' and t.prop  like'%{1}%'and t.prop  like'%{2}%'and t.prop  like'%{3}%'and t.prop  like'%{4}%'and t.prop  like'%{5}%'and t.prop  like'%{6}%'and t.prop  like'%{7}%'and t.prop  like'%{8}%'and t.prop  like'%{9}%' and t.prop  like'%{10}%' and t.FileName  like'%{11}%' and t.Remark  like'%{12}%' order by sn";
                    string sql = "select t.sn,t.prop, t.FileName,t.FDate,t.FBillOr,t.Remark from (select sn , TypeName+ZZName+RsName+WgName+ WLName+YBName+ColorName+MaterialName+WidthName+ZWName+TxName as prop,  FileName,FDate,FBillOr,Remark from dbo.ImageList)t where t.prop  like'%{0}%' and t.prop  like'%{1}%'and t.prop  like'%{2}%'and t.prop  like'%{3}%'and t.prop  like'%{4}%'and t.prop  like'%{5}%'and t.prop  like'%{6}%'and t.prop  like'%{7}%'and t.prop  like'%{8}%'and t.prop  like'%{9}%' and t.prop  like'%{10}%' and t.FileName  like'%{11}%' and t.Remark  like'%{12}%' order by sn";
                    sql = string.Format(sql, TypeName.Trim(), ZZName.Trim(), RsName.Trim(), WgName.Trim(), WLName.Trim(), YBName.Trim(), ColorName.Trim(), MaterialName.Trim(), WidthName.Trim(), ZWName.Trim(), TxName.Trim(), FileNamet.Trim(), remark.Trim());
                    DataSet MyDS_Grid = add.getDataSet(sql, "tb_Stuffbusic");
                    dataGridView1.DataSource = MyDS_Grid.Tables[0];
                    dataGridView1.AutoGenerateColumns = true;  //是否自动创建列
                    dataGridView1.Columns[0].Width = 35;
                    dataGridView1.Columns[1].Width = 400;
                    dataGridView1.Columns[2].Width = 100;
                    dataGridView1.Columns[3].Width = 70;
                    dataGridView1.Columns[4].Width = 60;
                    dataGridView1.Columns[5].Width = 60;
                    this.N_Previous.Enabled = true;
                    this.N_First.Enabled = true;
                    this.N_Next.Enabled = true;
                    this.N_Cauda.Enabled = true;
                }
            }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            private void button3_Click_1(object sender, EventArgs e)
            {
                button6_Click(null, null);
                button2.Enabled = true;
            }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            private void button9_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)  
{

    int i = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
    string sql1 = "delete from ImageData where sn='" + i + "'";
    string sql2 = "delete from dbo.ImageList where sn='" + i + "'";
    add.getsqlcom(sql1 + sql2);
    MessageBox.Show("编号第" + i + "条记录已被删除");
    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
    textBox1.Clear();
    pictureBox1.Image = null;  
  
}  
                
                
            }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            private void button7_Click(object sender, EventArgs e)
            {
                int i = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                string sql5 = "delete from ImageData where sn='" + i + "'";
                string sql6 = "delete from dbo.ImageList where sn='" + i + "'";
                add.getsqlcom(sql5 + sql6);
                //add.getsqlcom(sql6);
                //string connStr = "server=192.168.1.200;database=picturemanage;uid=sa;pwd=";
                using (SqlConnection conn = new SqlConnection(MyMeans.M_str_sqlcon))
                {
                   
                    string TypeName = lj(listView1);
                    string ZZName = lj(listView2);
                    string RsName = lj(listView3);
                    string WgName = lj(listView4);
                    string YBName = lj(listView5);
                    string WLName = lj(listView6);
                    string ColorName = lj(listView7);
                    string MaterialName = lj(listView8);
                    string WidthName = lj(listView9);
                    string ZWName = lj(listView10);
                    string TxName = lj(listView11);
                    DateTime FDate = DateTime.Now;
                    string FBillOr = this.label17.Text;
                    string Remark = this.textBox3.Text;
                    string FileNamet = this.textBox1.Text;
                    //string pic = "无图片";
                    string jbsx = TypeName + ZZName + RsName + WgName + WLName + YBName + ColorName + MaterialName + WidthName + ZWName + TxName;
                    if (jbsx == "")
                    {
                        MessageBox.Show("至少输入一项基本信息");

                    }
                    else
                    {
                        string sql1 = "insert ImageList(sn, TypeName, ZZName, RsName, WgName, WLName, YBName, ColorName, MaterialName, WidthName, ZWName, TxName, FDate, FBillOr, Remark, FileName )";
                        string sql2 = " values('" + i + "', '" + TypeName + "', '" + ZZName + "', '" + RsName + "', '" + WgName + "', '" + WLName + "', '" + YBName + "', '" + ColorName + "', '" + MaterialName + "', '" + WidthName + "', '" + ZWName + "', '" + TxName + "', '" + FDate + "', '" + FBillOr + "', '" + Remark + "', '" + FileNamet + "')";
                        string sql3 = sql1 + sql2;

                        //MyMeans add = new MyMeans();
                        add.getsqlcom(sql3);

                        if (this.pictureBox1.Image != null)
                        {
                            string sql = "Insert Into ImageData(sn,ImageList) Values (@sn,@ImgData) ";
                           // pictureBox1.Dispose();
                            byte[] imageBytes = GetBytes(pictureBox1.Image);

                            
                            //string sql5 = "delete from ImageData where sn='" + i + "'";
                            //add.getsqlcom(sql5);

                            using (SqlCommand cmd = new SqlCommand(sql))
                            {
                                SqlParameter param = new SqlParameter("ImgData", SqlDbType.VarBinary, imageBytes.Length);
                                SqlParameter sn = new SqlParameter("sn", SqlDbType.Int);
                                param.Value = imageBytes;

                                sn.Value = i;
                                cmd.Parameters.Add(param);
                                cmd.Parameters.Add(sn);
                                cmd.Connection = conn;
                                conn.Open();
                                int t = cmd.ExecuteNonQuery();
                                //pic = "有图片";
                                //MessageBox.Show(i.ToString() + "条有图片记录已修改！");
                                conn.Close();
                            }
                        }
                        MessageBox.Show( "图库库记录已修改！");
                        LisEdit(dataGridView1);
                        
                    }
                    ;
                }


            }

            private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                if (this.dataGridView1.CurrentCell == null)
                {
                    if (this.dataGridView1.Rows.Count > 0 && this.dataGridView1.Columns.GetColumnCount(DataGridViewElementStates.Visible) > 0)
                    {
                        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[this.dataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Visible).Index];
                    }
                }
                else
                {
                    DataGridViewCell cell = this.dataGridView1.CurrentCell;
                    int iIndex = -1;
                    if (e.KeyCode == Keys.Up)
                        iIndex = this.dataGridView1.Rows.GetPreviousRow(cell.RowIndex, DataGridViewElementStates.Visible);
                    else
                        iIndex = this.dataGridView1.Rows.GetNextRow(cell.RowIndex, DataGridViewElementStates.Visible);
                    if (iIndex >= 0)
                    {
                        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[iIndex].Cells[cell.ColumnIndex];
                    }
                }
                dataGridView1_CellContentClick(null, null);
            }
                
                         
                                            
                //if (e.KeyCode ==Keys.Up)
                //{
                //    int i = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                //    dataGridView1.
                

                //    dataGridView1_CellContentClick(null, null) ;
                //}
                //if (e.KeyCode == Keys.Down)
                //{
                //    //int i = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                //    //N_First_Click(sender, e);

                    //dataGridView1_CellContentClick(null, null);
                //}

            }

            private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
            {

            }

            private void timer1_Tick(object sender, EventArgs e)
            {
                toolStripStatusLabel3.Text = DateTime.Now.ToString() + "    ";
            }

            private void Formxz_FormClosing(object sender, FormClosingEventArgs e)
            {
                if (DialogResult.OK == MessageBox.Show("你确定要关闭应用程序吗？", "关闭提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    this.FormClosing -= new FormClosingEventHandler(this.Formxz_FormClosing);//为保证Application.Exit();时不再弹出提示，所以将FormClosing事件取消
                    Application.Exit();//退出整个应用程序
                }
                else
                {
                    e.Cancel = true;  //取消关闭事件
                }
            }
            public static MmEdit aa = null;
            private void 更改密码mToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (aa == null)
                {
                    aa = new MmEdit();
                    aa.ShowDialog();
                }
                else
                {
                    aa.Activate();
                    aa.WindowState = FormWindowState.Normal;
                }
            }
        /// <summary>
        /// 显示无图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            private void button11_Click(object sender, EventArgs e)
            {
                string sql = "select sn , TypeName+ZZName+RsName+WgName+ WLName+YBName+ColorName+MaterialName+WidthName+ZWName+TxName as prop,  FileName,FDate,FBillOr,Remark from dbo.ImageList  where sn not in (select sn from ImageData)  order by sn";
                DataSet MyDS_Grid = add.getDataSet(sql, "tb_Stuffbusic");
                dataGridView1.DataSource = MyDS_Grid.Tables[0];
                dataGridView1.AutoGenerateColumns = true;  //是否自动创建列
                dataGridView1.Columns[0].Width = 35;
                dataGridView1.Columns[1].Width = 400;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 70;
                dataGridView1.Columns[4].Width = 60;
                this.N_Previous.Enabled = true;
                this.N_First.Enabled = true;
                this.N_Next.Enabled = true;
                this.N_Cauda.Enabled = true;
                this.btnFirst.Enabled = false;
                this.btnNext.Enabled = false;
                this.btnPrevious.Enabled = false;
                this.btnLast.Enabled = false;
                this.btngoto.Enabled = false;
            }
            protected bool IsInteger(string value)
            {
                string pattern = @"^[0-9]*[1-9][0-9]*$";
                return Regex.IsMatch(value, pattern);
            }

            private void btngoto_Click(object sender, EventArgs e)
            {
                if (this.textgoto.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请输入要跳转的页码", "信息提示");
                    this.textgoto.Focus();
                    return;
                
                }
                if(!IsInteger(this.textgoto.Text.Trim()))
                {
                MessageBox.Show("跳转的页码必须是数字", "信息提示");
                this.textgoto.Focus();
                this.textgoto.SelectAll();
                return;
                }
                int pageindex = Convert.ToInt32(this.textgoto.Text.Trim());
                if (pageindex > objPagerManager.PageCount)
                {
                    MessageBox.Show("跳转的页码不能大于总页数", "信息提示");
                    this.textgoto.Focus();
                    this.textgoto.SelectAll();
                    return;
                }
                objPagerManager.CurrentPage = Convert.ToInt32(this.textgoto.Text.Trim());
                query();
                if (objPagerManager.CurrentPage == objPagerManager.PageCount)
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;

                }
            }

            private void btnFirst_Click(object sender, EventArgs e)
            {
                objPagerManager.CurrentPage = 1;
                query();
                this.btnPrevious.Enabled = false;
                this.btnFirst.Enabled = false;

            }

            private void btnNext_Click(object sender, EventArgs e)
            {
                objPagerManager.CurrentPage++;
                query();
                if (objPagerManager.CurrentPage == objPagerManager.PageCount)
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                
                }
            }

            private void btnPrevious_Click(object sender, EventArgs e)
            {
                objPagerManager.CurrentPage -= 1;
                query();
                if (objPagerManager.CurrentPage == 1)
                {
                    this.btnPrevious.Enabled = false;
                    this.btnFirst.Enabled = false;
                }
            }

            private void btnLast_Click(object sender, EventArgs e)
            {
                objPagerManager.CurrentPage = objPagerManager.PageCount;
                query();
                this.btnLast.Enabled = false;
                this.btnNext.Enabled = false;
            }

            private void dataGridView2_Paint(object sender, PaintEventArgs e)
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.ForeColor = Color.Blue;
                foreach (DataGridViewColumn col in this.dataGridView2.Columns)
                {
                    col.HeaderCell.Style = style;
                }
                this.dataGridView2.EnableHeadersVisualStyles = false;
                e.Graphics.DrawRectangle(Pens.CadetBlue, new Rectangle(0, 0, this.dataGridView2.Width - 1, this.dataGridView2.Height - 1));
            }

            private void btnStartvideo_Click(object sender, EventArgs e)
            {
                
                try
                {

                    this.pbImage.Visible = true;
                    this.pbImage.BringToFront();
                    this.objVideo = new Video(this.pbImage.Handle, this.pbImage.Left, this.pbImage.Top, this.pbImage.Width, (short)this.pbImage.Height);
                    objVideo.OpenVideo();
                    this.btnStartvideo.Enabled = false;
                    this.btnCloseVideo.Enabled=true;
                    this.btnTake.Enabled = true;

                    this.btnCloseVideo.BackColor = Color.Red;
                    this.btnTake.BackColor = Color.YellowGreen;
                    this.btnTake.ForeColor = Color.White;
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("摄像头启动失败！"+ex.Message,"提示信息");
                }

            }

            private void btnCloseVideo_Click(object sender, EventArgs e)
            {
                if (objVideo != null)
                {
                    this.objVideo.CloseVideo();
                }
                this.pbImage.Visible = false;
                this.btnStartvideo.Enabled = true;
                this.btnCloseVideo.Enabled = false;
                this.btnTake.Enabled = false;
                this.btnStartvideo.BackColor = Color.Green;
                this.btnStartvideo.ForeColor = Color.White;
                this.btnCloseVideo.BackColor = Color.Gray;
                this.btnCloseVideo.ForeColor = Color.White;
                this.btnTake.BackColor = Color.Gray;
                this.btnTake.ForeColor = Color.YellowGreen;


            }

            private void btnTake_Click(object sender, EventArgs e)
            {
                this.pictureBox1.Image = objVideo.CatchVideo();
            }
            private void lbldisplay()
            {
                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "123";
                        //item.ForeColor = Color.White;
                        //control.BackColor = Color.YellowGreen;

                    }
                }
            }

            private void pbImage_Paint(object sender, PaintEventArgs e)
            {
                PictureBox p = (PictureBox)sender;
                Pen pp = new Pen(Color.CadetBlue);
                e.Graphics.DrawRectangle(pp, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.X + e.ClipRectangle.Width - 1, e.ClipRectangle.Y + e.ClipRectangle.Height - 1);
            }


        }
        


    }

