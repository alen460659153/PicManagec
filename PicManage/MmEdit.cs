using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PicManage
{
    public partial class MmEdit : Form
    {
        public MmEdit()
        {
           
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             MyMeans bb = new MyMeans();
            string   mm=this.textBox1.Text;
            string name = MyMeans.Login_Name;
            string sql = "update CzrBaseTbl set PassWord='{0}' where CzrName='{1}'";
            sql=string.Format(sql,mm,name);
                        bb.getsqlcom(sql);
                        MessageBox.Show("密码已修改");
                        this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MmEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Formxz.aa = null;
        }
    }
}
