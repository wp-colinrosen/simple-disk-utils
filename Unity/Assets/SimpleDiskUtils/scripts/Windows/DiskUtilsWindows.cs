using System.Runtime.InteropServices;
using System.Text;

namespace SimpleDiskUtils
{
	public class DiskUtilsWindows : INativeDiskUtils
	{
		private const string DEFAULT_DRIVE = "C:/";


		/// <summary>
		/// Checks the available space.
		/// </summary>
		/// <returns>The available spaces in MB.</returns>
		/// <param name="drive">Disk name. For example, "C:/"</param>
		public int CheckAvailableSpace(string drive = null)
		{
			drive ??= DEFAULT_DRIVE;
			return getAvailableDiskSpace(new StringBuilder(drive));
		}

		/// <summary>
		/// Checks the total space.
		/// </summary>
		/// <returns>The total space in MB.</returns>
		/// <param name="drive">Disk name. For example, "C:/"</param>
		public int CheckTotalSpace(string drive = null)
		{
			drive ??= DEFAULT_DRIVE;
			return getTotalDiskSpace(new StringBuilder(drive));
		}

		/// <summary>
		/// Checks the busy space.
		/// </summary>
		/// <returns>The busy space in MB.</returns>
		/// <param name="drive">Disk name. For example, "C:/"</param>
		public int CheckBusySpace(string drive = null)
		{
			drive ??= DEFAULT_DRIVE;
			return getBusyDiskSpace(new StringBuilder(drive));
		}


		[DllImport("DiskUtilsWinAPI")]
		private static extern int getAvailableDiskSpace(StringBuilder drive);

		[DllImport("DiskUtilsWinAPI")]
		private static extern int getTotalDiskSpace(StringBuilder drive);

		[DllImport("DiskUtilsWinAPI")]
		private static extern int getBusyDiskSpace(StringBuilder drive);
	}
}