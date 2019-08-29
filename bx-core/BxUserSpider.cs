using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using bx_core.Model;
using Newtonsoft.Json.Linq;

namespace bx_core
{
    public class BxUserSpider : AbsSpider
    {
        #region 属性

        private BxApi _api;

        public BxApi Api
        {
            get => _api;
            set => _api = value;
        }

        #endregion

        //关键词队列
        private Queue<string> _queue = new Queue<string>();

        private BxConfig _config = null;
        private bool isRunning = false;
        private int finish = 0;

        public BxUserSpider(BxApi api)
        {
            _api = api;
        }


        /// <summary>
        /// 启动
        /// </summary>
        public void Run(BxConfig config)
        {
            Stop();
            _config = config;
            ThreadPool.SetMaxThreads(50, 50);
            //解析队列
            lock (_queue)
            {
                foreach (var word in _config.Keywords)
                {
                    _queue.Enqueue(word);
                }
            }

            finish = 0; //完成数
            for (var i = 0; i < config.ThreadCount; i++)
            {
                ThreadPool.QueueUserWorkItem(BackTask, config);
            }
        }

        /// <summary>
        /// 任务
        /// </summary>
        private void BackTask(object config)
        {
            while (true)
            {
                var word = "";
                //获取一个值
                lock (_queue)
                {
                    if (_queue.Count > 0)
                    {
                        word = _queue.Dequeue(); //出队列
                    }
                }

                if (word.Equals(""))
                {
                    break;
                }

                Debug.WriteLine("开始获取关键词：" + word);
                while (!word.Equals(""))
                {
                    //循环去后去获取信息
                    Task.Run(async () =>
                    {
                        var arc = 0;
                        while (true)
                        {
                            var res = await _api.YuerSearch(word, arc, 1000);
                            if (res.Ok)
                            {
                                var dt = res.Data as JObject;
                                var userList = dt.GetValue("result").ToObject<JObject>().GetValue("valueMap")
                                    .ToObject<JObject>()
                                    .GetValue("userList").ToObject<JObject>().GetValue("contentList")
                                    .ToObject<JArray>();
                                Console.WriteLine(userList);
                                var page = dt.GetValue("result").ToObject<JObject>().GetValue("anchor").ToObject<int>();
                                Console.WriteLine(page);
                                if (userList.Count <= 0)
                                {
                                    break;
                                }

                                foreach (var jToken in userList)
                                {
                                    var user = jToken.ToObject<BxUser>();
                                    Console.WriteLine(user);
                                    //继续获取用户信息
                                    var ret = await _api.BxUserCard(user.uid);
                                    if (ret.Ok)
                                    {
                                        user = (ret.Data as JObject)?.GetValue("result").ToObject<BxUser>();
                                        OnOnRecvUser(user);
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }

                            Thread.Sleep(100);
                            arc++;
                        }
                    }).Wait();
                    break;
                }
            }

            Debug.WriteLine("线程执行完毕");
            OnOnThreadFinish(this);
            finish++;
            //所有线程都执行完毕
            if (finish >= _config.ThreadCount)
            {
                OnOnFinish(finish);
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            isRunning = false;
            finish = 0;
        }
    }
}