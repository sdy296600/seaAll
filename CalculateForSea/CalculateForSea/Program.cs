using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculateForSea
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process[] procs = Process.GetProcessesByName("CalculateForSea");
            // 두번 이상 실행되었을 때 처리할 내용을 작성합니다.
            if (procs.Length > 1)
            {
                MessageBox.Show("프로그램이 이미 실행되고 있습니다.\n다시 한번 확인해주시기 바랍니다.");
                Application.Exit();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
