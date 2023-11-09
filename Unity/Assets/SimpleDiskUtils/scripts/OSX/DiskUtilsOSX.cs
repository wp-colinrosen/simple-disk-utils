using System.IO;
using System.Runtime.InteropServices;

namespace SimpleDiskUtils
{
	public class DiskUtilsOSX : INativeDiskUtils
	{
		public int CheckAvailableSpace(string drive = null)
		{
			return getAvailableDiskSpace();
		}

		public int CheckTotalSpace(string drive = null)
		{
			return getTotalDiskSpace();
		}

		public int CheckBusySpace(string drive = null)
		{
			return getBusyDiskSpace();
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