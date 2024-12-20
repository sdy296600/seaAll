using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitoringSystem.Model
{
    public class DataModel
    {
        private string? _itemNo;
        public string? ItemNo
        {
            get { return _itemNo; }
            set { _itemNo = value; }
        }

        private string? _isRunning;
        public string? IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; }
        }
        private string? _cycleTime;
        public string? CycleTime
        {
            get { return _cycleTime; }
            set { _cycleTime = value; }
        }
        private string? _warmUpCnt;
        public string? WarmUpCnt
        {
            get { return _warmUpCnt; }
            set { _warmUpCnt = value; }
        }
        private string? _okCnt;
        public string? OkCnt
        {
            get { return _okCnt; }
            set { _okCnt = value; }
        }
        private string? _errCnt;
        public string? ErrCnt
        {
            get { return _errCnt; }
            set { _errCnt = value; }
        }
        private string? _allCnt;
        public string? AllCnt
        {
            get { return _allCnt; }
            set { _allCnt = value; }
        }
    }
}
