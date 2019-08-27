using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using rv_core.Inters;
using rvcore.Model;

namespace rvcore.Http
{
    /// <inheritdoc />
    public class HttpHelper
    {
        private HttpHelper()
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
                var res = await RequestGetRes(client, req, 10);
                httpRes.Status = (int) res.StatusCode;
                if (res.IsSuccessStatusCode)
                {
                    var html = await res.Content.ReadAsStringAsync();
                    httpRes.Str = html;
                    httpRes.Ok = res.StatusCode == HttpStatusCode.OK; //默认code
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


        /// <summary>
        ///  请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="req"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> RequestGetRes(HttpClient client, HttpRequestMessage req,
            int timeout = 10)
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);
            var res = await client.SendAsync(req);
            //记录日志
            return res;
        }
    }
}