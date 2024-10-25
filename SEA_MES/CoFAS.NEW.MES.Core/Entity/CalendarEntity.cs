using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CoFAS.NEW.MES.Core.Entity
{
    public class CalendarEntity
    {
		public string CRUD { get; set; }
		public int id { get; set; }
		public string title { get; set; }
		public string content { get; set; }
		public DateTime start_date { get; set; }
		public DateTime end_date { get; set; }
		public DateTime reg_date { get; set; }
		public string reg_user { get; set; }
		public DateTime up_date { get; set; }
		public string up_user { get; set; }
		public int color_R { get; set; }
		public int color_G { get; set; }
		public int color_B { get; set; }
	}

}
