using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class Base_Searchbox : System.Windows.Forms.UserControl
    {
        DataTable _pDataTable = new DataTable();
        public Base_Searchbox(DataTable pDataTable)
        {
            _pDataTable = pDataTable;
            InitializeComponent();
        }

        public string SearchName
        {
            get { return Control_Name.Text; }
            set { Control_Name.Text = value; }
        }
        public string DisplayText
        {
            get{ return _DisplayText.Text;}
            set{ _DisplayText.Text = value;}
        }

        public string SearchText { get; set; } = "";

        private void button1_Click(object sender, EventArgs e)
        {
            BasePopupBox basePopupBox = new BasePopupBox(false);
            basePopupBox.Name = "Base_Searchbox";
            basePopupBox._pDataTable = _pDataTable;
            if(basePopupBox.ShowDialog() == DialogResult.OK)
            {
                _DisplayText.Text = basePopupBox._CD_NAME;
                SearchText = basePopupBox._CD;
            }
        }
    }
}
