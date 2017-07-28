using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance.Test.Common
{
    public class CalculateData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ThreadCount { get; set; }

        public int Samples { get; set; }

        public int Avg { get; set; }

        public int Percent90 { get; set; }

        public int Percent95 { get; set; }

        public int Percent99 { get; set; }

        public int Max { get; set; }

        public int Min { get; set; }

        public int Throughput { get; set; }

        public string Details { get; set; }
    }
}
