using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;

namespace PicManage
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmlogin());
          //  Application.Run(new frmnewlogin());
            //Application.Run(new Formxz());
        }
    }
}
