using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Performance.Test.Common
{
    public abstract class BaseTest
    {
        protected readonly ConcurrentBag<PerformanceData> lists = new ConcurrentBag<PerformanceData>();
        protected bool _isRunning = true;
        protected bool _isStart = false;

        protected int _threadCount;

        protected string url = "http://127.0.0.1:11563/";

        protected BaseTest(int threadCount)
        {
            this._threadCount = threadCount;
            Initialize();
            Console.WriteLine("Initialize successful.");
        }

        public virtual void Run()
        {
            Console.WriteLine($"begin run '{this.GetType().Name}' test");
            for (int i = 0; i < _threadCount; i++)
            {
                var thread = new Thread(async () =>
                {
                    var stop = new Stopwatch();
                    while (_isRunning)
                    {
                        if (!_isStart) Thread.Sleep(5);
                        else
                        {
                            stop.Start();
                            var id = await RunTest();
                            stop.Stop();
                            lists.Add(new PerformanceData(DateTime.Now, stop.ElapsedMilliseconds, id));
                            stop.Reset();
                        }
                    }
                });

                thread.Start();
            }

            Thread.Sleep(200);
            _isStart = true;

            Thread.Sleep(20000);
            _isStart = false;
            _isRunning = false;
            Thread.Sleep(1000);
            this.CalculatePerformance();
        }

        protected abstract void Initialize();

        protected abstract Task<string> RunTest();

        protected void CalculatePerformance()
        {
            if (lists.Count == 0)
            {
                Console.Write("Collect performance data is empty.");
                return;
            }

            var cal = new CalculateData();

            var name = this.GetType().Name;
            if (name.Length < 20) name = name.PadRight(20 - name.Length);
            cal.Name = name;

            cal.ThreadCount = _threadCount;
            cal.Samples = lists.Count;
            cal.Avg = (int)lists.Average(a => a.TimeStamp);


            int location90 = Convert.ToInt32(lists.Count * 0.90);
            int location95 = Convert.ToInt32(lists.Count * 0.95);
            int location99 = Convert.ToInt32(lists.Count * 0.99);

            cal.Percent90 = (int)lists.OrderBy(o => o.TimeStamp).Skip(location90).Take(1).FirstOrDefault().TimeStamp;
            cal.Percent95 = (int)lists.OrderBy(o => o.TimeStamp).Skip(location95).Take(1).FirstOrDefault().TimeStamp;
            cal.Percent99 = (int)lists.OrderBy(o => o.TimeStamp).Skip(location99).Take(1).FirstOrDefault().TimeStamp;
            cal.Max = (int)lists.Max(a => a.TimeStamp);
            cal.Min = (int)lists.Min(a => a.TimeStamp);

            var result = (from l in lists
                          group l by l.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") into g1
                          select new
                          {
                              Key = g1.Key,
                              Count = g1.Count()
                          }).ToList();

            result.RemoveAt(0);
            result = result.Take(result.Count - 1).ToList();

            cal.Throughput = (int)result.Average(a => a.Count);
            StringBuilder sb = new StringBuilder();
            foreach (var item in result)
            {
                sb.Append(item.Key + " " + item.Count + "|");
            }

            cal.Details = sb.ToString().TrimEnd('|');
            DapperHelper.Insert(cal);
            Console.WriteLine(cal.ToJson());
        }
    }
}
