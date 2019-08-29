using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using rv_core.Inters;
using rvcore.Model;

namespace rvcore.Http
{
    public class SysApi : IHttpRequest
    {
        private IHttpClientFactory _factory;

#if DEBUG
        //private readonly string _url_auth_upgrade = "http://127.0.0.1:9011/api/passport/upgrade";
        private readonly string _url_auth_upgrade = "http://blog.oeynet.com:9011/api/passport/upgrade";
#else
        private readonly string _url_auth_upgrade = "http://blog.oeynet.com:9011/api/passport/upgrade";
#endif


        public SysApi(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<HttpRes> CheckAuth()
        {
            var client = _factory.CreateClient("blog.oeynet.com");
            var req = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_url_auth_upgrade)
            };
            return await Request(client, req);
        }

        public async Task<HttpRes> Request(HttpClient client, HttpRequestMessage req)
        {
            var httpRes = new HttpRes {Data = null, Ok = false, Message = "REQUEST FAIL", Status = 200};
            httpRes.Url = req.RequestUri.ToString();
            httpRes.Method = req.Method.ToString();
            try
            {
                var res = await HttpHelper.RequestGetRes(client, req, 10);
                httpRes.Status = (int) res.StatusCode;
                if (res.IsSuccessStatusCode)
                {
                    var html = await res.Content.ReadAsStringAsync();
                    if (res.Content.Headers.ContentType.MediaType.ToLower().Equals("application/json"))
                    {
                        httpRes.Data = JsonConvert.DeserializeObject(html);
                        httpRes.IsJsonObject = true;
                        //判断是否请求成功
                        var dt = (JObject) httpRes.Data;
                        //是否成功
                        if (dt.TryGetValue("success", out var success))
                        {
                            httpRes.Ok = success.ToObject<bool>();
                        }

                        //消息
                        if (dt.TryGetValue("msg", out var msg))
                        {
                            httpRes.Message = msg.ToObject<string>();
                        }

                        //code
                        if (dt.TryGetValue("code", out var code))
                        {
                            httpRes.Code = code.ToObject<string>();
                        }
                    }
                    else
                    {
                        httpRes.Str = html;
                    }
                }
            }
            catch (Exception ex)
            {
                httpRes.Ok = false;
                httpRes.Message = "请求失败：" + ex.Message;
            }
            finally
            {
                Console.WriteLine("\nRequest:" + httpRes.Message);
            }

            return httpRes;
        }
    }
}