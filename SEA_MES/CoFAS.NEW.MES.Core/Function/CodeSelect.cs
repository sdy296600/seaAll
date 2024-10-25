using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoFAS.NEW.MES.Core.Function
{
    public class CodeSelect
    {
        public CodeSelect() { }
        public static DataTable ComboBoxSet(string pServiceName)
        {
            DataTable _DataTable = null;
            try
            {
                // 엔티티선언
                CodeSelect_Entity _Entity = new CodeSelect_Entity();
                _Entity.ServiceName = pServiceName;


                _DataTable = new CoreBusiness().xComboBox(_Entity);
            }
            catch (Exception pException)
            {
                //CustomMsg.ShowMessage(pException.ToString(), "Error");
            }
            return _DataTable;
        }
     
        public static DataTable ComboBoxSetting(string serviceName, string parameter1, string parameter2, string parameter3)
        {
            DataTable _DataTable = null;
            try
            {
                // 엔티티선언

                _DataTable = new CoreBusiness().ComboBoxSetting(serviceName, parameter1, parameter2, parameter3);
            }
            catch (Exception pException)
            {
                CustomMsg.ShowMessage(pException.ToString(), "Error");
            }
            return _DataTable;
        }
    }
}
