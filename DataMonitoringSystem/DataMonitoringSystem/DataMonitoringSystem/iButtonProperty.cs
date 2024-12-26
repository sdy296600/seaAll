using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataMonitoringSystem
{
    internal interface iButtonProperty
    {
        public Visibility BackVisibilty { get; set; }
        public Visibility NextVisibilty { get; set; }

    }
}
