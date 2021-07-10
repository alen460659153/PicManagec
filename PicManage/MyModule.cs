using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PicManage
{
    class MyModule
    {
        

        public void Ena_Button(Button B1, Button B2, Button B3, Button B4, int n1, int n2, int n3, int n4)
        {
            B1.Enabled = Convert.ToBoolean(n1);
            B2.Enabled = Convert.ToBoolean(n2);
            B3.Enabled = Convert.ToBoolean(n3);
            B4.Enabled = Convert.ToBoolean(n4);
        }
    }
}
