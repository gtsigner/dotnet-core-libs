﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rv_core.Utils
{
    public static class Rander
    {
        /// <summary>
        /// 淘宝的可用用户ID
        /// </summary>
        private static readonly string[] UserIds =
        {
            "",
            "",
        };

        /// <summary>
        /// 淘宝的可用用户ID
        /// </summary>
        public static string[] Nicknames =
        {
            "JrLabadie", "小可爱111199tb", "PhDStoltenberg", "DVMHowe", "SrKovacek", "IHayes", "IIGrimes", "MDThiel",
            "DDSZemlak",
            "小默默12333", "IMcKenzie", "SrMcDermott", "JrProsacco", "IVGottlieb", "MDBarton", "VBashirian", "IIIJohns",
            "DVMDietrich", "asdasda123213", "VFerry", "SrWhite", "MDHaley", "SrConnelly", "SrAbernathy", "PhDBednar",
            "DVMKling", "fgrgdbdfb234", "PhDLockman", "IVMcLaughlin", "IIIRosenbaum", "IIIJenkins", "JrMiller",
            "SrWisozk",
            "IMarquardt", "MDLittle", "helanzhu", "IIIBayer", "MDOkuneva", "SrKuhn", "MDStehr", "JrArmstrong",
            "琪305878061", "语堂491302524", "鹏涛976553388", "伟宸2102232712", "涛2419862044", "浩然9402116197", "熠彤5052498593",
            "楷瑞6404477584", "振家7825824103", "乐驹8341514529", "笑愚6923269795", "浩然5311164914", "智渊1250823524",
            "致远4119197193", "立果6115971710", "弘文9744818174", "远航3671065542", "SrCollier",
            "DDSBrown", "SrSchuppe", "DDSHalvorson", "ILeffler", "SrBauch", "IRolfson", "MDHills",
            "VReichert", "PhDSchmidt", "DVMLarson", "MDKautzer", "MDTerry", "DVMWaelchi", "JrMann", "JrSchroeder",
            "JrMills", "VKuhn", "SrFay", "SrWillms", "IIWelch", "DDSFarrell", "IKuvalis", "PhDKemmer", "IIJohns",
            "PhDO'Kon", "IIIJerde", "PhDUpton", "DDSGoyette", "IIIWindler", "IIDaugherty", "IIHirthe", "IReinger",
            "IVWillms", "MDLubowitz", "DDSTillman", "IFay", "IIBogan", "IIICollier", "MDMosciski", "IIIMraz", "IVKunde",
            "JrStehr", "DVMSchneider", "IIWyman", "DVMKlein", "SrBode", "IIShields", "IVZboncak", "IVSipes",
            "PhDBeahan", "JrKeeling", "JrTillman", "SrMarvin", "IIHuels", "JrShields", "MDBecker", "DVMBrakus", "VKihn",
            "IFay", "JrOberbrunner", "PhDHamill", "JrBalistreri", "ISmitham", "DDSO'Reilly", "SrKirlin", "IIITowne",
            "MDWiegand", "PhDBatz", "IIReinger", "MDConnelly", "IIWard", "DDSWehner", "VMurray", "SrPollich",
            "PhDMonahan", "VConroy", "SrO'Reilly", "MDSchulist", "IVKulas", "DVMRempel", "IIWunsch", "MDParker",
            "PhDSchmitt", "IVHaag", "VMueller", "IVKoss", "VWintheiser", "godtoy", "摸摸UUI", "老大哥1231", "小信息12312",
            "I靖琪文", "PhD炫明立果", "PhD靖琪文博", "DDS鑫磊鸿煊", "V梓晨风华", "Jr13123123",
            "tb",
            "tb",
            "tb",
            "tb",
        };

        /// <summary>
        /// 创建一个随机用户名
        /// </summary>
        /// <returns></returns>
        public static string RandNickname()
        {
            var random = new Random(DateTime.Now.Millisecond);
            var value = Nicknames[random.Next(0, Nicknames.Length - 2)].ToLower();
            if (value.Contains("tb"))
            {
                value += random.Next(10000000, 999999999).ToString();
            }

            return value;
        }

        public static string RandFanLevel()
        {
            var random = new Random(DateTime.Now.Millisecond);
            return random.Next(1, 11).ToString();
        }

        /// <summary>
        /// 生产一个淘宝的升级用户ID
        /// </summary>
        /// <returns></returns>
        public static string RandTbUserId()
        {
            var random = new Random(DateTime.Now.Millisecond);
            return random.Next(111111, 555555555).ToString();
        }

        private static readonly int[] Prices = {5, 10, 15, 20, 100, 150, 250, 1000};

        /// <summary>
        /// 随机礼物的价格
        /// </summary>
        /// <returns></returns>
        public static int RandGiftPrice()
        {
            var t = DateTime.Now.ToString("fff");
            var random = new Random(int.Parse(t));
            return Prices[random.Next(0, 5)];
        }
    }
}