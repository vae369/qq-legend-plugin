using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 传奇
{
    internal class Program
    {
        /// <summary>
        /// 应用程序的主入口点.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             
            //Application.Run(new FrmMagicTest());


            Application.Run(new FormMain());
        }

    }
}
