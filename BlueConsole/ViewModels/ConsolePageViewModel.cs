using BlueConsole.Commands;
using BlueConsole.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;
using ValueType = BlueConsole.Commands.ValueType;

namespace BlueConsole.ViewModels
{
    public partial class ConsolePageViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        public partial IDevice? Device { get; set; }

        public ObservableCollection<LogItem> Logs { get; set; } = [];

        public ObservableCollection<ConsoleCommand> Commands { get; set; } = [];

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Device = query["Device"] as IDevice;
            Logs.Add(new LogItem("11111"));

            Commands.Add(new SimpleCommand()
            {
                Name = "直接指令1",
                Payload = "rdsstest",
                Description = "在双方的首发"
            });

            Commands.Add(new SimpleCommand()
            {
                Name = "直接指令2",
                Payload = "rdsstest loop",
                Description = "dsadasdasd"
            });

            Commands.Add(new SimpleCommand()
            {
                Name = "直接指令3",
                Payload = "rdsstest stop"
            });

            Commands.Add(new EnumCommand()
            {
                Name = "枚举命令1",
                Options = [new EnumOption() { Name="选项1",Payload = "1"}, new EnumOption() { Name = "选项2", Payload = "2" }]
            });

            Commands.Add(new EnumCommand()
            {
                Name = "枚举命令2",
                Options = [new EnumOption() { Name = "开", Payload = "1" }, new EnumOption() { Name = "关", Payload = "2" }]
            });

            Commands.Add(new EnumCommand()
            {
                Name = "枚举命令3",
                Options = [new EnumOption() { Name = "启动", Payload = "1" }, new EnumOption() { Name = "停止", Payload = "2" }]
            });

            Commands.Add(new InputCommand()
            {
                Name = "输入命令1，整数",
                ValueType = ValueType.Int
            });

            Commands.Add(new InputCommand()
            {
                Name = "输入命令2，浮点",
                ValueType = ValueType.Double
            });

            Commands.Add(new InputCommand()
            {
                Name = "输入命令3，字符串",
                ValueType = ValueType.String
            });
        }


        [RelayCommand]
        public void AddLog()
        {
            Logs.Add(new LogItem(@"====== OEM T Recorder======
Softvar:02.04.00.01
Hardvar:02.03.00.00

ID:    	 1710     SN:       	    0
Cycle:	 1s
Delay:	 500ms    PgaSize:	 0

Temp calib time:
Temp coef:
T0 = 0.000000E+00
T1 = 0.000000E+00
T2 = 0.000000E+00
T3 = 0.000000E+00
T4 = 0.000000E+00


slable
llable
"));



            // Logs.Add(new LogItem("111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111"));
        }


    }
}
