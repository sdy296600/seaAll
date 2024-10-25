using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 수불현황 : BaseForm1
    {
        public 수불현황()
        {
            InitializeComponent();
        }
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"　SELECT  *	
                                   FROM 
                                   (
                             　SELECT  A.ID				as	'A.ID'				  	 
                                      ,A.OUT_CODE		as	'A.OUT_CODE'		  		 
                                      ,A.IN_STOCK_DATE	as	'A.IN_STOCK_DATE'	  	 
                                      ,A.IN_TYPE		as	'A.IN_TYPE'		  	                       
                                      ,A.STOCK_MST_ID	as	'A.STOCK_MST_ID'	  
                                      ,B.NAME	        as  'B.NAME'	        	  
                                      ,B.OUT_CODE		as	'B.OUT_CODE'		  		 
                                      ,B.STANDARD		as	'B.STANDARD'		  		 
                                      ,B.TYPE			as	'B.TYPE'			  		 
                                      ,A.IN_QTY			as	'A.IN_QTY'			  	 			 
                                      ,A.COMMENT		as	'A.COMMENT'		  	 
                                      ,A.COMPLETE_YN	as	'A.COMPLETE_YN'	  	 
                                      ,A.USE_YN			as	'A.USE_YN'			  		                           
                                      ,A.REG_USER		as	'A.REG_USER'		  		 
                                      ,A.REG_DATE		as	'A.REG_DATE'		  		 
                                      ,A.UP_USER		as	'A.UP_USER'		  	 
                                      ,A.UP_DATE		as	'A.UP_DATE'		  	 
                                FROM [dbo].[IN_STOCK_DETAIL] A
                                INNER JOIN [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID
                                WHERE 1=1 AND A.USE_YN = 'Y'
                     UNION ALL
　　　　　　　　　　　　　　　SELECT   A.ID				as	'A.ID'				  
                                      ,A.OUT_CODE		as	'A.OUT_CODE'		   
                                      ,A.OUT_STOCK_DATE	as	'A.IN_STOCK_DATE'	   
                                      ,A.OUT_TYPE		as	'A.IN_TYPE'		 	                         
                                      ,A.STOCK_MST_ID	as	'A.STOCK_MST_ID'	  
                                      ,B.NAME	        as  'B.NAME'	       	  
                                      ,B.OUT_CODE		as	'B.OUT_CODE'		  			 
                                      ,B.STANDARD		as	'B.STANDARD'		  			 
                                      ,B.TYPE			as	'B.TYPE'			  		 
                                      ,A.OUT_QTY		as	'A.IN_QTY'			   			 		 
                                      ,A.COMMENT		as	'A.COMMENT'		 	  
                                      ,A.COMPLETE_YN	as	'A.COMPLETE_YN'	 	  
                                      ,A.USE_YN			as	'A.USE_YN'			                             
                                      ,A.REG_USER		as	'A.REG_USER'		   
                                      ,A.REG_DATE		as	'A.REG_DATE'		   
                                      ,A.UP_USER		as	'A.UP_USER'		 	  
                                      ,A.UP_DATE		as	'A.UP_DATE'		 	  
                                FROM [dbo].[OUT_STOCK_DETAIL] A
                                INNER JOIN [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID
                                WHERE 1=1 AND A.USE_YN = 'Y'
                                 ) A
								 WHERE 1=1";
                               
                StringBuilder sb = new StringBuilder();

                Function.Core.GET_WHERE_UNION(this._PAN_WHERE, sb);

                string sql = str + sb.ToString()
                    + this._pMenuSettingEntity.BASE_WHERE
                    + this._pMenuSettingEntity.BASE_ORDER;


                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpMain);
            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
            finally
            {
                DevExpressManager.SetCursor(this, Cursors.Default);
            }
        }
    }
}
