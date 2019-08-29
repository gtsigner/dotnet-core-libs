using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Fancs.Annotations;

namespace Fancs.ControlWindows.Bx
{
    public class BxViewModel : INotifyPropertyChanged
    {
        private bool _isRunning = false;
        private readonly StringBuilder _logStringBuilder = new StringBuilder();
        private long _count = 0; //采集的条数
        private string _accountStr = "17311301741-zhaojunlike"; //账号密码信息
        private string _keywords = "";
        private string _udid = "c64a11a9c335422790085bf5b3efba0f"; //设备信息

        public string Udid
        {
            get => _udid;
            set
            {
                _udid = value;
                OnPropertyChanged(nameof(Udid));
            }
        }

        public string AccountStr
        {
            get => _accountStr;
            set
            {
                _accountStr = value;
                OnPropertyChanged(nameof(AccountStr));
            }
        }

        public string Keywords
        {
            get => _keywords;
            set
            {
                _keywords = value;
                OnPropertyChanged(nameof(Keywords));
            }
        }

        private long _total = 0;

        public long Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        public long Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        /// <summary>
        /// log——text
        /// </summary>
        public string LogText
        {
            get => _logStringBuilder.ToString();
            set
            {
                if (value == String.Empty || _logStringBuilder.Length > 5000)
                {
                    _logStringBuilder.Clear();
                }

                var dateStr = DateTime.Now.ToLocalTime().ToString(CultureInfo.CurrentCulture);
                _logStringBuilder.Append($"{dateStr} --- {value}\n");
                OnPropertyChanged(nameof(LogText));
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                OnPropertyChanged(nameof(IsStop));
                OnPropertyChanged(nameof(IsRunning));
            }
        }

        public bool IsStop => !_isRunning;


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}