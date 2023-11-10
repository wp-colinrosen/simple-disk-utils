using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDiskUtils
{
	public class DiskUtilsWindows : INativeDiskUtils
	{
		private const string DEFAULT_DRIVE = "C:/";


		public Task<int> CheckAvailableSpace(string drive = null)
		{
			drive ??= DEFAULT_DRIVE;
			var result = getAvailableDiskSpace(new StringBuilder(drive));
			return Task.FromResult(result);
		}

		public Task<int> CheckTotalSpace(string drive = null)
		{
			drive ??= DEFAULT_DRIVE;
			var result = getTotalDiskSpace(new StringBuilder(drive));
			return Task.FromResult(result);
		}

		public Task<int> CheckBusySpace(string drive = null)
		{
			drive ??= DEFAULT_DRIVE;
			var result = getBusyDiskSpace(new StringBuilder(drive));
			return Task.FromResult(result);
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