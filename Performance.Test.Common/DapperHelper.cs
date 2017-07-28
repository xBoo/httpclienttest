using System;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Performance.Test.Common
{
    public class DapperHelper
    {
        static string _connectionString;

        static DapperHelper()
        {
            //_connectionString = "";
        }

        public static void Insert(CalculateData data)
        {
            //using (var conn = new SqlConnection(_connectionString))
            //{
            //    conn.Open();
            //    conn.Execute("INSERT INTO [CalculateData] (Name, ThreadCount, Samples, Avg, Percent90, Percent95, Percent99, Max, Min, Throughput, Details) VALUES (@Name, @ThreadCount, @Samples, @Avg, @Percent90, @Percent95, @Percent99, @Max, @Min, @Throughput, @Details)", data);
            //    conn.Close();
            //}
        }
    }
}
