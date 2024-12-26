using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class frmPrintSetting : Form
    {
        public string printName;
        public frmPrintSetting()
        {
            InitializeComponent();
            printName = Properties.Settings.Default.printName;
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   
        public List<string> LoadPrinters()
        {
            List<string> items = new List<string>();

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                items.Add(printer);
            }
            return items;
        }

        private void SetPrinter(string printerName)
        {
            // 프린터의 유효성 검사
            if (PrinterSettings.InstalledPrinters.Cast<string>().Any(p => p.Equals(printerName, StringComparison.OrdinalIgnoreCase)))
            {
                Properties.Settings.Default.printName = printerName;
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("유효하지 않은 프린터입니다.");
            }
        }



        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                SetPrinter(listBox1.Items[listBox1.SelectedIndex].ToString());
                MessageBox.Show("프린터가 성공적으로 설정되었습니다.", "설정 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("프린터를 선택해 주세요.", "선택 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmPrintSetting_Load(object sender, EventArgs e)
        {
            List<string> printers = LoadPrinters();
            listBox1.Items.Clear();
            foreach (string printer in printers) 
            {
                listBox1.Items.Add(printer);
            }
        }
    }
}
