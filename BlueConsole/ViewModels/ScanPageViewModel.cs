using BlueConsole.Helpers;
using BlueConsole.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System.Collections.ObjectModel;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace BlueConsole.ViewModels
{
    public partial class ScanPageViewModel : ObservableObject
    {
        private readonly IBluetoothLE _bluetoothLE;
        private readonly IAdapter _adapter;
        private readonly ScanFilterOptions _filterOptions;
        public ObservableCollection<DeviceWrapper> FiltedDevice { get; set; } = [];

        public ScanPageViewModel()
        {
            _bluetoothLE = CrossBluetoothLE.Current;
            _adapter = _bluetoothLE.Adapter;

            _adapter.ScanMode = ScanMode.LowLatency;
            _adapter.ScanTimeout = 60000;
            _adapter.DeviceDiscovered += (s, e) => FiltedDevice.Add(new DeviceWrapper(_adapter, e.Device));
            _filterOptions = new ScanFilterOptions();
            //_adapter.ScanTimeoutElapsed += AdapterScanTimeoutElapsed;
        }

        [RelayCommand]
        public void Refresh()
        {
            fds();
        }

        private async void fds()
        {
            var permissionResult = await CheckAndRequestBluetoothPermissions();
            if (permissionResult != PermissionStatus.Granted) return;

            if (!_bluetoothLE.IsOn)
            {
                Toast.Show("蓝牙未开启，请手动开启蓝牙。");
                return;
            }

            FiltedDevice.Clear();
            await _adapter.StartScanningForDevicesAsync(_filterOptions, DeviceFilter);

        }

        private bool DeviceFilter(IDevice device)
        {
            if(string.IsNullOrEmpty(device.Name))
            {
                return false;
            }
            return true;
        }

        private void HandleDeviceDiscovered(object? sender, DeviceEventArgs args)
        {

        }


        public async Task<PermissionStatus> CheckAndRequestBluetoothPermissions()
        {
            PermissionStatus status;
            status = await Permissions.CheckStatusAsync<Bluetooth>();

            if (status == PermissionStatus.Granted)
                return status;

            status = await Permissions.RequestAsync<Bluetooth>();
            return status;
        }

        
    }
}
