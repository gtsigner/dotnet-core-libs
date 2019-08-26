using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using rvcore.Model;

namespace rvcore.Http
{
    public class HttpHelper
    {
        public HttpHelper()
        {
        }

        /// <summary>
        /// 发送http请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public static async Task<HttpRes> Request(HttpClient client, HttpRequestMessage req)
        {
            var httpRes = new HttpRes {Data = null, Ok = false, Message = "REQUEST FAIL", Status = 200};
            try
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                var res = await client.SendAsync(req);
                httpRes.Status = (int) res.StatusCode;
                if (res.IsSuccessStatusCode)
                {
                    var html = await res.Content.ReadAsStringAsync();
                    Console.WriteLine($"URI：{req.RequestUri}\tHTML:{html}");
                    var data = JObject.Parse(html);
                    httpRes.Data = data;
                    if (data.TryGetValue("msg", out var msg))
                    {
                        httpRes.Message = (string) msg;
                    }
                    else
                    {
                        httpRes.Message = res.StatusCode.ToString();
                    }

                    httpRes.Ok = res.StatusCode == HttpStatusCode.OK; //默认code
                    //判断是否成功
                    if (data.TryGetValue("code", out var code))
                    {
                        httpRes.Ok = (code.ToObject<string>() == "A00000");
                        httpRes.Message = data.GetValue("msg")?.ToObject<string>();
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
                Console.WriteLine($"URI：{req.RequestUri} \t RespMessage:" + httpRes.Message + "\tStatus=" +
                                  httpRes.Status);
            }

            return httpRes;
        }
    }
}