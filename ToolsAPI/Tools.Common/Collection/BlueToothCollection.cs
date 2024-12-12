using InTheHand.Net.Sockets;

namespace Tools.Common.Collection
{
    public class BlueToothCollection
    {
        /// <summary>
        /// 用于存取获取到的蓝牙设备集合
        /// </summary>
        public static IReadOnlyCollection<BluetoothDeviceInfo> DiscoverDevices { get; set; }
    }
}
