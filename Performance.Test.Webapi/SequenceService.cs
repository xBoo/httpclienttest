using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Performance.Test.Webapi
{
    public static class SequenceService
    {
        private static string _machineNo;
        private static DateTime _originDatetime;
        public static DateTime OriginDatetime => _originDatetime;
        public static string MachineNo => _machineNo;

        static int _incrementAtoms = 1;
        static object _lockObj = new object();

        const int _limit = 99999999;

        static SequenceService()
        {
            _machineNo = "08";
            _originDatetime = new DateTime(1970, 1, 1);
        }

        public static int GenerateSequence()
        {
            lock (_lockObj)
            {
                if (_incrementAtoms++ > _limit)
                    _incrementAtoms = 1;

                return _incrementAtoms;
            }
        }
    }
}