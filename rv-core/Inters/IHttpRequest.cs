using System.Net.Http;
using System.Threading.Tasks;
using rvcore.Model;

namespace rv_core.Inters
{
    public interface IHttpRequest
    {
        Task<HttpRes> Request(HttpClient client, HttpRequestMessage req);
    }
}