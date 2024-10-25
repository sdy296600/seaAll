using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Function;
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
    public partial class Base_ComboBox : System.Windows.Forms.UserControl
    {
        public Base_ComboBox(DataRow row,bool isALL)
        {
            InitializeComponent();
            _SearchCom.AddValue(new CoreBusiness().Spread_ComboBox(row["CODETYPE"].ToString(), row["CODENAME"].ToString(), ""), 0, 0, "", isALL);
        }
        public Base_ComboBox()
        {
            InitializeComponent();
        }
          
        public string SearchName
        {
            get { return Control_Name.Text; }
            set { Control_Name.Text = value; }
        }
        public string SearchText
        {
            get { return _SearchCom.GetValue(); }
          
        }

        private void _SearchCom_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
