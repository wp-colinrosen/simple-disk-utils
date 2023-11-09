using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleDiskUtils
{
	public class DiskUtilsWindows : INativeDiskUtils
	{
		private const string DEFAULT_DRIVE = "C:/";


		public int CheckAvailableSpace(string drive = null)
		{
			drive ??= DEFAULT_DRIVE;
			return getAvailableDiskSpace(new StringBuilder(drive));
		}

		public int CheckTotalSpace(string drive = null)
		{
			drive ??= DEFAULT_DRIVE;
			return getTotalDiskSpace(new StringBuilder(drive));
		}

		public int CheckBusySpace(string drive = null)
		{
			drive ??= DEFAULT_DRIVE;
			return getBusyDiskSpace(new StringBuilder(drive));
		}

		public string[] GetDrives()
		{
			return Directory.GetLogicalDrives();
		}


		[DllImport("DiskUtilsWinAPI")]
		private static extern int getAvailableDiskSpace(StringBuilder drive);

		[DllImport("DiskUtilsWinAPI")]
		private static extern int getTotalDiskSpace(StringBuilder drive);

		[DllImport("DiskUtilsWinAPI")]
		private static extern int getBusyDiskSpace(StringBuilder drive);
	}
}