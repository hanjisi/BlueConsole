namespace BlueConsole.Helpers
{
    public class BluetoothLeHelper
    {
        public static string ParseBluetoothMacAddress(Guid guid)
        {
            // 将GUID转换为字节数组
            byte[] guidBytes = guid.ToByteArray();

            // 提取最后6个字节（48位），这是蓝牙MAC地址的长度
            byte[] macBytes = new byte[6];
            Array.Copy(guidBytes, 10, macBytes, 0, 6);

            // 将字节数组转换为蓝牙MAC地址的字符串表示
            string bluetoothMacAddress = BitConverter.ToString(macBytes).Replace("-", ":").ToLower();

            return bluetoothMacAddress;
        }
    }
}
