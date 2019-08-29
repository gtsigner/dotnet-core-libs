using System.Net.Http;
using System.Threading.Tasks;
using rv_core.Inters;
using rvcore.Http;
using rvcore.Model;

namespace rvcore.Api
{
    public class AuthApi : IHttpRequest
    {
        /// <summary>
        /// 进行请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<HttpRes> Request(HttpClient client, HttpRequestMessage req)
        {
            return await HttpHelper.Request(client, req);
        }
    }
}