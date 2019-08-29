using System;
using Newtonsoft.Json;

namespace rvcore.Model
{
    public class HttpRes
    {
        private string _message = "请求失败";
        private string _code = "550";
        private string _url = "";
        private string _method = "";

        public string Url
        {
            get => _url;
            set => _url = value;
        }

        public string Method
        {
            get => _method;
            set => _method = value;
        }

        public string Code
        {
            get => _code;
            set => _code = value;
        }

        private bool _ok = false;
        private object _data = null;
        private string _str = "";
        private bool _isJsonObject = false;

        public bool IsJsonObject
        {
            get => _isJsonObject;
            set => _isJsonObject = value;
        }

        public string Str
        {
            get => _str;
            set => _str = value;
        }


        private int _status = 500;

        public int Status
        {
            get => _status;
            set => _status = value;
        }

        public string Message
        {
            get => _message;
            set => _message = value;
        }


        public bool Ok
        {
            get => _ok;
            set => _ok = value;
        }

        public object Data
        {
            get => _data;
            set => _data = value;
        }

        public HttpRes()
        {
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}