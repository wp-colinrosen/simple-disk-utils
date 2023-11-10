using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SimpleDiskUtils
{
	public class DiskUtilsIOS : INativeDiskUtils
	{
		public Task<int> CheckAvailableSpace(string drive = null)
		{
			var ret = getAvailableDiskSpace();
			var result = int.Parse(ret.ToString());
			return Task.FromResult(result);
		}

		public Task<int> CheckTotalSpace(string drive = null)
		{
			var ret = getTotalDiskSpace();
			var result = int.Parse(ret.ToString());
			return Task.FromResult(result);
		}

		public Task<int> CheckBusySpace(string drive = null)
		{
			var ret = getBusyDiskSpace();
			var result = int.Parse(ret.ToString());
			return Task.FromResult(result);
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