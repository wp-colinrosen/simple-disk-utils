using System.IO;
using System.Runtime.InteropServices;

namespace SimpleDiskUtils
{
	public class DiskUtilsIOS : INativeDiskUtils
	{
		public int CheckAvailableSpace(string drive = null)
		{
			var ret = getAvailableDiskSpace();
			return int.Parse(ret.ToString());
		}

		public int CheckTotalSpace(string drive = null)
		{
			var ret = getTotalDiskSpace();
			return int.Parse(ret.ToString());
		}

		public int CheckBusySpace(string drive = null)
		{
			var ret = getBusyDiskSpace();
			return int.Parse(ret.ToString());
		}
		
		public string[] GetDrives()
		{
			return Directory.GetLogicalDrives();
		}


		[DllImport("__Internal")]
		private static extern ulong getAvailableDiskSpace();

		[DllImport("__Internal")]
		private static extern ulong getTotalDiskSpace();

		[DllImport("__Internal")]
		private static extern ulong getBusyDiskSpace();
	}
}