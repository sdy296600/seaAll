using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.DPS
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
     
            //return;
            Process[] procs = Process.GetProcessesByName("CoFAS.NEW.MES.DPS");
            // 두번 이상 실행되었을 때 처리할 내용을 작성합니다.
            if (procs.Length > 1)
            {
                //MessageBox.Show("프로그램이 이미 실행되고 있습니다.\n다시 한번 확인해주시기 바랍니다.");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MQTT_DPS());
             Application.Run(new OPC_UA_dps());
        }
    }
}
