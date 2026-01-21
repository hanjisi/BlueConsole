using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlueConsole.Views
{
    public class LogItem : INotifyPropertyChanged
    {
        public DateTime Time { get; set; }

        public byte[] Raw { get; set; }

        string _displayText;
        public string DisplayText
        {
            get => _displayText;
            private set
            {
                _displayText = value;
                OnPropertyChanged();
            }
        }

        public void UpdateDisplay(bool hex)
        {
            var timePrefix = $"[{Time:HH:mm:ss.fff}] ";

            if (hex)
            {
                DisplayText = timePrefix + ToHex(Raw);
            }
            else
            {
                DisplayText = FormatTextWithTime(Raw, timePrefix);
            }
        }

        static string FormatTextWithTime(byte[] raw, string timePrefix)
        {
            var text = Encoding.ASCII.GetString(raw)
                .Replace("\r\n", "\n")
                .TrimEnd('\n');

            var lines = text.Split('\n');

            if (lines.Length == 0)
                return timePrefix;

            var sb = new StringBuilder();

            // 第一行带时间
            sb.Append(timePrefix);
            sb.AppendLine(lines[0]);

            // 后续行直接顶到最左边
            for (int i = 1; i < lines.Length; i++)
            {
                sb.AppendLine(lines[i]);
            }

            return sb.ToString().TrimEnd();
        }

        static string ToHex(byte[] raw, int width = 16)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < raw.Length; i++)
            {
                sb.Append(raw[i].ToString("X2")).Append(' ');

                if ((i + 1) % width == 0)
                    sb.AppendLine();
            }

            return sb.ToString().TrimEnd();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
