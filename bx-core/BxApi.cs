using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using rv_core.Inters;
using rv_core.Utils;
using rvcore.Http;
using rvcore.Model;
using Encoder = rv_core.Utils.Encoder;

namespace bx_core
{
    public class BxUrls
    {
        public static readonly string PasswordLoginUrl = "https://api.hibixin.com/passport/login/v1/mobile";
        public static readonly string BxCardUrl = "https://api.hibixin.com/bxuser/v1/query/chatroom/card";
        public static readonly string BxYuerSearch = "https://api.hibixin.com/search/v2/yuerSearch";

        public static readonly string BxChatRooms =
            "https://api.bxyuer.com/chatroom/v1/chatroom/home/tab/content/chatroom";

        public static readonly string BxChatRoomOnlineList = "https://api.bxyuer.com/chatroom/v1/chatroom/online/list";
    }

    /// <summary>
    /// 比心的API
    /// </summary>
    public class BxApi : IHttpRequest
    {
        #region Proto

        private string _password = "";
        private string _account = "";
        private string _deviceId;
        private string _udid = ""; //

        public string Udid
        {
            get => _udid;
            set => _udid = value;
        }

        private string _accessToken = "";

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string Account
        {
            get => _account;
            set => _account = value;
        }


        public string AccessToken
        {
            get => _accessToken;
            set => _accessToken = value;
        }

        #endregion


        public static readonly string UserAgent = "mapi/1.0 (Android 24;com.yupaopao.yuer 1.3.0.1;Xiaomi MI+5;y-xm)";
        public static readonly string SignKey = "ryE5sW5hbmRyy2lkNDEkkYr";
        private IHttpClientFactory _factory;


        public BxApi(IHttpClientFactory factory)
        {
            _factory = factory;
            _deviceId = CreateDeviceId();
        }

        /// <summary>
        /// 创建设备ID
        /// </summary>
        /// <returns></returns>
        public static string CreateDeviceId()
        {
            return CryptUtil.Md5Encode(Guid.NewGuid().ToString());
        }


        /// <summary>
        /// 使用密码进行登录
        /// </summary>
        /// <returns></returns>
        public async Task<HttpRes> PassWordLogin()
        {
            if (_account.Equals("") || _password.Equals("")) return new HttpRes {Message = "用户名或者密码参数不足"};
            var jb = new JObject
            {
                {"deviceId", _deviceId},
                {"mobile", _account},
                {"password", CryptUtil.Md5Encode(_password)},
                {"udid", _udid},
            };
            var jsonStr = JsonConvert.SerializeObject(jb);
            var signStr = Sign(jsonStr);
            var headers = GetHeaders(signStr);
            var req = new HttpRequestMessage {RequestUri = new Uri(BxUrls.PasswordLoginUrl), Method = HttpMethod.Post};
            foreach (var keyValuePair in headers)
            {
                req.Headers.Add(keyValuePair.Key, keyValuePair.Value.ToObject<string>());
            }

            req.Content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var client = _factory.CreateClient("api.hibixin.com");
            var res = await Request(client, req);
            if (res.Ok)
            {
                var dt = res.Data as JObject;
                var accessToken = dt?.GetValue("result").ToObject<JObject>().GetValue("accessToken").ToObject<string>();
                _accessToken = accessToken;
                Console.WriteLine("登录成功：" + accessToken);
            }

            return res;
        }

        /// <summary>
        /// 比心用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<HttpRes> BxUserCard(string toUid)
        {
            var jsonStr = "";
            var signStr = Sign(jsonStr);
            var headers = GetHeaders(signStr);
            var req = new HttpRequestMessage
                {RequestUri = new Uri(BxUrls.BxCardUrl + $"?toUid={toUid}"), Method = HttpMethod.Get};
            foreach (var keyValuePair in headers)
            {
                req.Headers.Add(keyValuePair.Key, keyValuePair.Value.ToObject<string>());
            }

            var client = _factory.CreateClient("api.hibixin.com");
            var res = await Request(client, req);
            return res;
        }

        //获取房间
        /// <summary>
        /// 比心用户信息
        /// https://api.bxyuer.com/chatroom/v1/chatroom/online/list?anchor=20&limit=20&roomId=135cf465203929dbd8bd73f94d8129e5
        /// </summary>
        /// <returns></returns>
        public async Task<HttpRes> ChatRoomOnlineUsers(string roomId, int anchor = 0, int limit = 0)
        {
            var jsonStr = "";
            var signStr = Sign(jsonStr);
            var headers = GetHeaders(signStr);
            var req = new HttpRequestMessage
            {
                RequestUri = new Uri(BxUrls.BxChatRoomOnlineList + $"?roomId={roomId}&anchor={anchor}&limit={limit}"),
                Method = HttpMethod.Get
            };
            foreach (var keyValuePair in headers)
            {
                req.Headers.Add(keyValuePair.Key, keyValuePair.Value.ToObject<string>());
            }

            var client = _factory.CreateClient("api.hibixin.com");
            var res = await Request(client, req);
            return res;
        }

        //获取房间
        /// <summary>
        /// 比心用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<HttpRes> ChatRooms(int pageNo = 0, int pageSize = 100, int tabId = 0)
        {
            var jsonStr = "";
            var signStr = Sign(jsonStr);
            var headers = GetHeaders(signStr);
            var req = new HttpRequestMessage
            {
                RequestUri = new Uri(BxUrls.BxChatRooms + $"?pageNo={pageNo}&pageSize={pageSize}&tabId={tabId}"),
                Method = HttpMethod.Get
            };
            foreach (var keyValuePair in headers)
            {
                req.Headers.Add(keyValuePair.Key, keyValuePair.Value.ToObject<string>());
            }

            var client = _factory.CreateClient("api.hibixin.com");
            var res = await Request(client, req);
            return res;
        }


        //获取房间的在线用户


        /// <summary>
        /// 比心用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<HttpRes> YuerSearch(string keywords, long page = 0, int limit = 100)
        {
            var jb = new JObject
            {
                {"anchor", page},
                {"keyword", keywords},
                {"limit", limit},
                {"type", 1},
            };
            var jsonStr = JsonConvert.SerializeObject(jb);
            var signStr = Sign(jsonStr);
            var headers = GetHeaders(signStr);
            var req = new HttpRequestMessage
                {RequestUri = new Uri(BxUrls.BxYuerSearch), Method = HttpMethod.Post};
            foreach (var keyValuePair in headers)
            {
                req.Headers.Add(keyValuePair.Key, keyValuePair.Value.ToObject<string>());
            }

            req.Content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var client = _factory.CreateClient("api.hibixin.com");
            var res = await Request(client, req);
            return res;
        }


        private JObject GetHeaders(string signStr)
        {
            var p2 = Logger.GetTimeStampMic() + "";
            var p5 = "wifi";
            var job = new JObject
            {
                {"X-Udid", _udid},
                {"X-Client-Time", p2},
                {"X-Sign", signStr},
                {"X-AccessToken", _accessToken},
                {"X-NetWork", p5},
                {"X-User-Agent", UserAgent},
            };
            var headStr = Sign(JsonConvert.SerializeObject(job));
            job["X-Authentication"] = headStr;
            job["User-Agent"] = "okhttp/3.11.0";
            return job;
        }


        /// <summary>
        /// 构造签名
        /// </summary>
        /// <param name="signStr"></param>
        /// <returns></returns>
        public static string Sign(string signStr)
        {
            var str = Encoder.EncodeBase64(signStr);
            str = CryptUtil.Md5Encode(str + SignKey);
            return str;
        }

        public async Task<HttpRes> Request(HttpClient client, HttpRequestMessage req)
        {
            var httpRes = new HttpRes {Data = null, Ok = false, Message = "REQUEST FAIL", Status = 200};
            try
            {
                httpRes.Url = req.RequestUri.ToString();
                httpRes.Method = req.Method.ToString();
                var res = await HttpHelper.RequestGetRes(client, req);
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