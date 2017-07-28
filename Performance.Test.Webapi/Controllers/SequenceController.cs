using Performance.Test.Common;
using System;
using System.Web.Http;

namespace Performance.Test.Webapi.Controllers
{
    public class SequenceController : ApiController
    {
        [HttpGet()]
        public BizResult<string> Get()
        {
            var timestamp = (int)(DateTime.Now - SequenceService.OriginDatetime).TotalSeconds;
            var incr = SequenceService.GenerateSequence();

            return new BizResult<string>(string.Concat(new string[] { timestamp.ToString(), SequenceService.MachineNo, incr.ToString().PadLeft(8, '0') }));
        }
    }
}
