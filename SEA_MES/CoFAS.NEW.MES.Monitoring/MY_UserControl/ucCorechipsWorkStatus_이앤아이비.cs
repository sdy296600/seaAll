using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Windows.Forms;

namespace CoFAS.NEW.MES.Monitoring
{
    public partial class ucCorechipsWorkStatus_이앤아이비 : UserControl
    {

        public DataRow _dataRow = null;


        public ucCorechipsWorkStatus_이앤아이비(DataRow dataRow )
        {
            _dataRow = dataRow;
            InitializeComponent();

        }
        public ucCorechipsWorkStatus_이앤아이비()
        {
           
            InitializeComponent();
            this.Load -= ucCorechipsWorkStatus_Load;
   
            lbl_equipment_name.ForeColor = Color.White;
            lbl_mold_name.ForeColor = Color.White;
            //lbl_product_code.ForeColor = Color.White;
            lbl_product_name.ForeColor = Color.White;
            lbl_start_work_time.ForeColor = Color.White;
            lbl_worker_name.ForeColor = Color.White;
            lbl_plan_qty.ForeColor = Color.White;
            lbl_all_qty.ForeColor = Color.White;

            this.BackColor = Color.FromArgb(47, 54, 83);
        }
        private void ucCorechipsWorkStatus_Load(object sender, EventArgs e)
        {

            data_set();
        }

    
        public void data_set( )
        {
            try
            {
                lbl_equipment_name.Text = _dataRow[0].ToString();

                if(_dataRow[4] == DBNull.Value)
                {
                    this.BackColor = Color.FromArgb(170,170,170);
           

                    lbl_mold_name.Text = "-";

                    //lbl_product_code.Text = "-";

                    lbl_product_name.Text = "-";

                    lbl_start_work_time.Text = "-";

                    lbl_worker_name.Text = "-";

                    lbl_plan_qty.Text = "-";

                    lbl_all_qty.Text = "-";
                }
                else
                {
                    if(_dataRow[1] != DBNull.Value)
                    {
                        lbl_mold_name.Text = _dataRow[1].ToString();
                    }
                    else
                    {
                        lbl_mold_name.Text = "-";
                    }

                    if (_dataRow[2] != DBNull.Value)
                    {
                        lbl_product_name.Text = _dataRow[2].ToString();
                    }
                    if (_dataRow[3] != DBNull.Value)
                    {
                        lbl_start_work_time.Text = _dataRow[3].ToString();
                    }
                    //if (_dataRow[4] != DBNull.Value)
                    //{
                    //    lbl_start_work_time.Text = Convert.ToDateTime(_dataRow[4]).ToString("MM-dd HH:mm:ss");
                    //}

                    if (_dataRow[4] != DBNull.Value)
                    {
                        lbl_worker_name.Text = _dataRow[4].ToString();
                    }

                    if (_dataRow[5] != DBNull.Value)
                    {
                        lbl_plan_qty.Text = _dataRow[5].ToString();
                    }

                    if (_dataRow[6] != DBNull.Value)
                    {
                        lbl_all_qty.Text = _dataRow[6].ToString();
                    }
                    //if (_dataRow[7] != DBNull.Value)
                    //{
                    //    lbl_all_qty.Text = Convert.ToDecimal(_dataRow[7]).ToString("N0");
                    //}

                }
             
                          

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
              
            }

        }

  
     
    }
}
