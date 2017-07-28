using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance.Test.Common
{
    public class PerformanceData
    {
        public DateTime CreateTime { get; set; }

        public long TimeStamp { get; set; }

        public string Id { get; set; }

        public PerformanceData(DateTime createTime, long timeStamp, string id)
        {
            this.CreateTime = createTime;
            this.TimeStamp = timeStamp;
            this.Id = id;
        }
    }
}
