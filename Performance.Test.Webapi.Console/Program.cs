using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance.Test.Webapi.Console
{
    class Program
    {
        static string path;

        static void Main(string[] args)
        {
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Performance.txt");
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            while (true)
            {
                System.Console.WriteLine("please input tasks count");
                int threadCount;
                try
                {
                    threadCount = int.Parse(System.Console.ReadLine());
                }
                catch (Exception)
                {
                    break;
                }

                new WebapiWithPerCallTest(threadCount).Run();
                new WebapiWithPerSessionTest(threadCount).Run();
            }
            System.Console.ReadLine();
        }

        public static void WriteToFile(string content)
        {
            File.AppendAllText(path, "******************************************************************\r\n", Encoding.UTF8);
            File.AppendAllText(path, content, Encoding.UTF8);
        }
    }
}
