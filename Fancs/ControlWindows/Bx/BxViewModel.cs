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