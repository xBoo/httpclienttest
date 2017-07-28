using System.Net.Http;
using System.Threading.Tasks;
using Performance.Test.Common;

namespace Performance.Test.Webapi.Console
{
    public class WebapiWithPerSessionTest : BaseTest
    {
        HttpClient client = null;
        public WebapiWithPerSessionTest(int threadCount) : base(threadCount)
        {
        }

        protected override void Initialize()
        {
            client = new HttpClient { BaseAddress = new System.Uri(url) };
            client.GetAsync("api/sequence").Wait();
        }

        protected override async Task<string> RunTest()
        {
            var response = await client.GetAsync("api/sequence");
            var content = await response.Content.ReadAsStringAsync();
            return content.FromJsonTo<BizResult<string>>().ReturnObj;
        }
    }
}