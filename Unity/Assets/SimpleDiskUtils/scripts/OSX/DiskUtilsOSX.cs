using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SimpleDiskUtils
{
	public class DiskUtilsOSX : INativeDiskUtils
	{
		public Task<int> CheckAvailableSpace(string drive = null)
		{
			var result = getAvailableDiskSpace();
			return Task.FromResult(result);
		}

		public Task<int> CheckTotalSpace(string drive = null)
		{
			var result = getTotalDiskSpace();
			return Task.FromResult(result);
		}

		public Task<int> CheckBusySpace(string drive = null)
		{
			var result = getBusyDiskSpace();
			return Task.FromResult(result);
		}

		public string[] GetDrives()
		{
			return Directory.GetLogicalDrives();
		}


		[DllImport("diskutils")]
		private static extern int getAvailableDiskSpace();

		[DllImport("diskutils")]
		private static extern int getTotalDiskSpace();

		[DllImport("diskutils")]
		private static extern int getBusyDiskSpace();
	}
}