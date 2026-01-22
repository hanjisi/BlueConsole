using BlueConsole.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;

namespace BlueConsole.ViewModels
{
    public partial class ConsolePageViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        public partial IDevice? Device { get; set; }

        public ObservableCollection<LogItem> Logs { get; set; } = [];


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Device = query["Device"] as IDevice;
            Logs.Add(new LogItem("11111"));
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
