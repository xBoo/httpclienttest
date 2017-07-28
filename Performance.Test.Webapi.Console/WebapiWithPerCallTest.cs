using System.Net.Http;
using System.Threading.Tasks;
using Performance.Test.Common;

namespace Performance.Test.Webapi.Console
{
    public class WebapiWithPerCallTest : BaseTest
    {
       

        public WebapiWithPerCallTest(int threadCount) : base(threadCount)
        {
        }

        protected override void Initialize()
        {
            using (var client = new HttpClient { BaseAddress = new System.Uri(url) })
            {
                var response = client.GetAsync("api/sequence").Result;
                var content = response.Content.ReadAsStringAsync().Result;
            }
        }

        protected override async Task<string> RunTest()
        {
            using (HttpClient client = new HttpClient { BaseAddress = new System.Uri(url) })
            {
                var response = await client.GetAsync("api/sequence");
                var content = await response.Content.ReadAsStringAsync();
                return content.FromJsonTo<BizResult<string>>().ReturnObj;
            }
        }
    }
}