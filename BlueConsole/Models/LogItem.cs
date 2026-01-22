using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlueConsole.Models
{
    public class LogItem : ObservableObject
    {
        //private readonly byte[] raw;
        public TimeSpan Time { get; } = DateTime.Now.TimeOfDay;


        public string? DisplayText { get; set; } // => FormatTextWithTime(raw, "");

        public LogItem(string raw)
        {
            this.DisplayText = $"{Time}: {raw}";
        }
        
        //public void UpdateDisplay(bool hex)
        //{
        //    var timePrefix = $"[{Time:HH:mm:ss.fff}] ";

        //    if (hex)
        //    {
        //        DisplayText = timePrefix + ToHex(Raw);
        //    }
        //    else
        //    {
        //        DisplayText = FormatTextWithTime(Raw, timePrefix);
        //    }
        //}

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

        //static string ToHex(byte[] raw)
        //{
        //    var sb = new StringBuilder();

        //    for (int i = 0; i < raw.Length; i++)
        //    {
        //        sb.Append(raw[i].ToString("X2")).Append(' ');
        //    }

        //    return sb.ToString().TrimEnd();
        //}
    }
}
