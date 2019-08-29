using System;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace rv_core.Utils
{
    /// <summary>
    /// 获取MAC地址
    /// </summary>
    public class MacUtil
    {
        /// <summary>
        /// 所有的Mac设备
        /// </summary>
        public static string GetNetworkInterfaces()
        {
            var nibs = NetworkInterface.GetAllNetworkInterfaces();
            if (nibs.Length < 1)
            {
                return "";
            }

            var jar = new JArray();
            foreach (var adapter in nibs)
            {
                //以太网
                jar.Add(new JObject
                {
                    {"name", adapter.Name},
                    {"id", adapter.Id},
                });
            }

            return JsonConvert.SerializeObject(jar);
        }

        /// <summary>
        /// 获取以太网的ID
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetMacByName(string name = "以太网")
        {
            var nibs = NetworkInterface.GetAllNetworkInterfaces();
            if (nibs.Length < 1)
            {
                return "";
            }

            var jar = new JArray();
            foreach (var adapter in nibs)
            {
                //以太网
                if (adapter.Name.Equals(name))
                {
                    return adapter.Id;
                }
            }

            return "";
        }
    }
}