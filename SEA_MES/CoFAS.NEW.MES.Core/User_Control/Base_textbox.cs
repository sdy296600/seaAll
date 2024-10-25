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
    public partial class Base_textbox : System.Windows.Forms.UserControl
    {
        public Base_textbox()
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
            get{ return _SearchText.Text;}
            set{ _SearchText.Text = value;}
        }
    }
}
