using System;

namespace rv_core.Utils
{
    public class Logger
    {
        public static void Debug(string tag, object log)
        {
            Console.WriteLine($"{tag}.{log}");
        }

        /// <summary> 
        /// 获取秒级别时间错
        /// </summary> 
        /// <returns></returns> 
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary> 
        /// 获取秒级别时间错
        /// </summary> 
        /// <returns></returns> 
        public static long GetTimeStampInt()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        /// <summary> 
        /// 获取毫秒级别时间错
        /// </summary> 
        /// <returns></returns> 
        public static long GetTimeStampMin()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }

        /// <summary> 
        /// 获取毫秒级别时间错
        /// </summary> 
        /// <returns></returns> 
        public static long GetTimeStampMic()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }
}