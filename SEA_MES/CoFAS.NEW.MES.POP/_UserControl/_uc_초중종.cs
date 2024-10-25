using CoFAS.NEW.MES.Core.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP._UserControl
{
    public partial class _uc_초중종 : UserControl
    {
        public string _pTYPE  = string.Empty;
        public string _pLOT  = string.Empty;

        public string _p품번 = string.Empty;

        public string _p실적 = string.Empty;
        public _uc_초중종()
        {
            InitializeComponent();

        }



        public void DATE_SET(DataTable dataTable)
        {
            foreach (DataRow item in dataTable.Rows)
            {
                if (item["TYPE"].ToString() == _pTYPE)
                {
                    string SEQ = item["SEQ"].ToString();

                    switch (SEQ)
                    {
                        case "1":
                            btn_1.Text = item["VALUE"].ToString();
                            break;
                        case "2":
                            btn_2.Text = item["VALUE"].ToString();
                            break;
                        case "3":
                            btn_3.Text = item["VALUE"].ToString();
                            break;
                        case "4":
                            btn_4.Text = item["VALUE"].ToString();
                            break;
                        case "5":
                            btn_5.Text = item["VALUE"].ToString();
                            break;
                        default:
                            break;
                    }
                }

            }
            //return;
            //if(_pTYPE == "Loss")
            //{
            //    if (btn_1.Text == "입력")
            //    {
            //        btn_1.Text = "1.2";
            //    }
            //    if (btn_2.Text == "입력")
            //    {
            //        btn_2.Text = "1.2";
            //    }
            //    if (btn_3.Text == "입력")
            //    {
            //        btn_3.Text = "1.2";
            //    }
            //    if (btn_4.Text == "입력")
            //    {
            //        btn_4.Text = "1.2";
            //    }
            //    if (btn_5.Text == "입력")
            //    {
            //        btn_5.Text = "1.2";
            //    }

            //}
        }

        public void _uc_초중종_Load(object sender, EventArgs e)
        {
            btn_1.Name = btn_1.Name + $"_{_pTYPE}";
            btn_2.Name = btn_2.Name + $"_{_pTYPE}";
            btn_3.Name = btn_3.Name + $"_{_pTYPE}";
            btn_4.Name = btn_4.Name + $"_{_pTYPE}";
            btn_5.Name = btn_5.Name + $"_{_pTYPE}";
        }                              
    }
}
