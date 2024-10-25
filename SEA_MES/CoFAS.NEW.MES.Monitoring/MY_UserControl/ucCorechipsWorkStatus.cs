using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Windows.Forms;

namespace CoFAS.NEW.MES.Monitoring
{
    public partial class ucCorechipsWorkStatus : UserControl
    {

        public DataRow _dataRow = null;


        public ucCorechipsWorkStatus(DataRow dataRow )
        {
            _dataRow = dataRow;
            InitializeComponent();

        }
        public ucCorechipsWorkStatus()
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
            lbl_Now_qty.ForeColor = Color.White;
            lbl_collection_date.ForeColor = Color.White;
            lbl_ratio.ForeColor         = Color.White;
            lbl_weight.ForeColor = Color.White;

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

                    lbl_Now_qty.Text = "-";

                    lbl_collection_date.Text = "-";

                    lbl_ratio.Text = "-";

                    lbl_weight.Text = "-";
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
                        //lbl_product_code.Text = _dataRow[2].ToString();
                    }
                    if (_dataRow[3] != DBNull.Value)
                    {
                        lbl_product_name.Text = _dataRow[3].ToString();
                    }
                    if (_dataRow[4] != DBNull.Value)
                    {
                        lbl_start_work_time.Text = Convert.ToDateTime(_dataRow[4]).ToString("MM-dd HH:mm:ss");
                    }
                    if (_dataRow[5] != DBNull.Value)
                    {
                        lbl_worker_name.Text = _dataRow[5].ToString();
                    }

                    if (_dataRow[6] != DBNull.Value)
                    {
                        lbl_plan_qty.Text = Convert.ToDecimal(_dataRow[6]).ToString("N0");
                    }
                    if (_dataRow[7] != DBNull.Value)
                    {
                        lbl_all_qty.Text = Convert.ToDecimal(_dataRow[7]).ToString("N0");
                    }
                    if (_dataRow[8] != DBNull.Value)
                    {
                        lbl_Now_qty.Text = Convert.ToDecimal(_dataRow[8]).ToString("N0");
                    }


                    if (_dataRow[10] != DBNull.Value)
                    {
                        lbl_ratio.Text = _dataRow[10].ToString();
                    }
                    else
                    {
                        lbl_ratio.Text = "-";
                    }


                    if (_dataRow[12] != DBNull.Value)
                    {
                        lbl_weight.Text = _dataRow[12].ToString();

                        if (_dataRow[13] != DBNull.Value)
                        {
                            if (_dataRow[12].ToString() != "분석중")
                            {
                                double ai_qty = Convert.ToDouble(_dataRow[12]);
                                double qty = Convert.ToDouble(_dataRow[13]);
                                double p = qty * 0.03;


                                if ((qty + p) < ai_qty)
                                {
                                    lbl_weight.ForeColor = Color.OrangeRed;
                                }
                                else if ((qty - p) > ai_qty)
                                {
                                    lbl_weight.ForeColor = Color.OrangeRed;
                                }
                                else
                                {
                                    //lbl_weight.BackColor = Color.FromArgb(197, 217, 241);
                                }
                            }

                        }

                    }
                    else
                    {
                        lbl_weight.Text = "-";
                    }

                    if (_dataRow[9] == DBNull.Value)
                    {
                        lbl_collection_date.Text = "-";
       
                        this.BackColor = Color.FromArgb(197, 217, 241);
                    }
                    else
                    {                    
                        DateTime date = Convert.ToDateTime(_dataRow[9]);

                        lbl_collection_date.Text = date.ToString("HH:mm:ss");

                        if (DateTime.Now > date.AddMinutes(5))
                        {
                            this.BackColor = Color.OrangeRed;
                        }
                        else
                        {
                            //if (_dataRow[11] != DBNull.Value)
                            //{
                            //    if (Convert.ToDecimal(_dataRow[11]) < 80)
                            //    {
                            //        this.BackColor = Color.LightYellow;
                            //    }
                            //    else
                            //    {
                            //        this.BackColor = Color.FromArgb(197, 217, 241);
                            //    }
                            //}
                            //else
                            //{
                            //    this.BackColor = Color.FromArgb(197, 217, 241);
                            //}
                            this.BackColor = Color.FromArgb(197, 217, 241);

                        }
                    }

         
                }
             
                          

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
              
            }

        }

  
     
    }
}
