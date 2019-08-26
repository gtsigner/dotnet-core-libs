using System;

namespace rvcore.Model
{
    public class HttpRes
    {
        private string _message = "";
        private int _code = 200;
        private bool _ok = false;
        private object _data = null;


        private int _status = 200;

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

        public int Code
        {
            get => _code;
            set => _code = value;
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
    }
}