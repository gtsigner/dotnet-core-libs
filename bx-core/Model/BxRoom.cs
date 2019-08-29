using Newtonsoft.Json;

namespace bx_core.Model
{
    public class BxRoom
    {
        /// <summary>
        /// 
        /// </summary>
        public string roomId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int roomNo { get; set; }

        /// <summary>
        /// [踢保]樱木:最爱范德彪徐诚小鹿
        /// </summary>
        public string roomTitle { get; set; }

        /// <summary>
        /// 交友
        /// </summary>
        public string roomTag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string roomTagColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int templet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int chatRoomId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int hasPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int peopleLimit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int onlineUserCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mixed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Ext ext { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class OwnerModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string uid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string accId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int yppNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 厅冠刷主持💕
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int gender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string age { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string diamondVipLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string diamondVipLevelName { get; set; }
    }


    public class Ext
    {
        /// <summary>
        /// 
        /// </summary>
        public int isTop { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string topColor { get; set; }

        /// <summary>
        /// 交友
        /// </summary>
        public string topTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string coverTagImgKey { get; set; }

        /// <summary>
        /// 羽墨 🔰
        /// </summary>
        public string currentHostDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int isBusy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string isCollection { get; set; }
    }
}