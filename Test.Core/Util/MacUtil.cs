using System;
using NUnit.Framework;
using rv_core.Utils;

namespace Test.Core.Util
{
    [TestFixture]
    public class MacUtil
    {
        [Test]
        public void interfaces()
        {
            var res = rv_core.Utils.MacUtil.GetNetworkInterfaces();
            Console.WriteLine(res);
            var mac = rv_core.Utils.MacUtil.GetMacByName();
            mac = CryptUtil.Md5Encode(mac);
            Console.WriteLine("MAC:" + mac);
        }
    }
}