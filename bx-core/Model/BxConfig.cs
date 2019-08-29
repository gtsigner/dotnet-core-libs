using System.Collections.Generic;
using Newtonsoft.Json;

namespace bx_core.Model
{
    public class BxConfig
    {
        private int _threadCount = 5;

        public int ThreadCount
        {
            get => _threadCount;
            set => _threadCount = value;
        }

        private List<string> _keywords = new List<string>();

        public List<string> Keywords
        {
            get => _keywords;
            set => _keywords = value;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}