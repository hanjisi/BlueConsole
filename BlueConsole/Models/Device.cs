using BlueConsole.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System.Collections.ObjectModel;

namespace BlueConsole.Models
{
    public partial class DeviceWrapper : ObservableObject
    {
        private readonly IAdapter _adapter;
        private IDevice _device;

        [ObservableProperty]
        public partial string? Name { get; set; }

        [ObservableProperty]
        public partial string? Address { get; set; }

        [ObservableProperty]
        public partial int Rssi { get; set; }

        [ObservableProperty]
        public partial DeviceState State { get; set; }

        public ObservableCollection<LogItem> Logs = [];
       
        public DeviceWrapper(IAdapter adapter, IDevice device)
        {
            _adapter = adapter;
            _device = device;
            _adapter.DeviceBondStateChanged += HandleDeviceBondStateChanged;
            _adapter.DeviceAdvertised += HandleDeviceAdvertised;
            _device = device;
            Address = BluetoothLeHelper.ParseBluetoothMacAddress(_device.Id);
            Update();
        }

        [RelayCommand]
        public async Task Conect()
        {
            var parameter = new Dictionary<string, object>
            {
                { "Device", _device }
            };
            await Shell.Current.GoToAsync($"ConsolePage", parameter);
        }

        private void HandleDeviceBondStateChanged(object? sender, DeviceBondStateChangedEventArgs args)
        {
            if (args.Device.Id != _device.Id) return;
            Update();
        }

        private void HandleDeviceAdvertised(object? sender, DeviceEventArgs args)
        {
            if (args.Device.Id != _device.Id) return;
            Update();
        }

        private void Update()
        {
            Name = string.IsNullOrEmpty(_device.Name) ? "未知名称" : _device.Name;
            Rssi = _device.Rssi;
            State = _device.State;
        }

        //private void HandleDeviceConnectionError(object? sender, DeviceErrorEventArgs args)
        //{
        //    if (args.Device.Id != _device.Id) return;
        //}
        //private void HandleDeviceConnectionLost(object? sender, DeviceErrorEventArgs args)
        //{
        //    if (args.Device.Id != _device.Id) return;
        //}
        //private void HandleDeviceConnected(object? sender, DeviceEventArgs args)
        //{
        //    if(args.Device.Id != _device.Id) return;
        //    Toast.Show($"设备已连接: {args.Device.Name}");
        //}
        //private void HandleDeviceDisconnected(object? sender, DeviceEventArgs args)
        //{
        //    if(args.Device.Id != _device.Id) return;
        //    Toast.Show($"设备已断开: {args.Device.Name}");

        //    _adapter.DeviceConnectionError -= HandleDeviceConnectionError;
        //    _adapter.DeviceConnectionLost -= HandleDeviceConnectionLost;
        //    _adapter.DeviceConnected -= HandleDeviceConnected;
        //    _adapter.DeviceDisconnected -= HandleDeviceDisconnected;
        //}
    }
}
