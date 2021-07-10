using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PicManage
{
    public partial class frmlogin : Form
    {
        MyMeans MyClass = new MyMeans();
        public frmlogin()
        {
            InitializeComponent();
        }

        private void frmlogin_Activated(object sender, EventArgs e)
        {
            textName.Focus();
        }
        private void textName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                textPass.Focus();
        }

        private void textPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                butLogin.Focus();
        }

        private void F_Login_Activated(object sender, EventArgs e)
        {
            textName.Focus();
        }

        private void butLogin_Click(object sender, EventArgs e)
        {
            if (textName.Text != "" & textPass.Text != "" & DateTime.Now<=Convert.ToDateTime("3029-12-10"))
            {
                SqlDataReader temDR = MyClass.getcom("select * from CzrBaseTbl where CzrName='" + textName.Text.Trim() + "' and PassWord='" + textPass.Text.Trim() + "'");
                bool ifcom = temDR.Read();
                if (ifcom)
                {
                    MyMeans.Login_Name = textName.Text.Trim();
                    MyMeans.Login_ID = temDR.GetString(0);
                    MyMeans.Login_edit =temDR.GetString(3);
                    MyMeans.My_con.Close();
                    MyMeans.My_con.Dispose();
                    //MyMeans.Login_n = (int)(this.Tag);
                    this.Hide();
                    Formxz myform = new Formxz();
                    myform.Show();
                    
                    
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //textName.Text = "";
                    //textPass.Text = "";
                }
                MyClass.con_close();
            }
            else
                MessageBox.Show("请将登录信息添写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
