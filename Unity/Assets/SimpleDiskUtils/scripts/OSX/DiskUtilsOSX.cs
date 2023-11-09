using System.Runtime.InteropServices;

namespace SimpleDiskUtils
{
	public class DiskUtilsOSX : INativeDiskUtils
	{
		/// <summary>
		/// Checks the available space.
		/// </summary>
		/// <returns>The available space in MB.</returns>
		public int CheckAvailableSpace(string drive = null)
		{
			return getAvailableDiskSpace();
		}

		/// <summary>
		/// Checks the total space.
		/// </summary>
		/// <returns>The total space in MB.</returns>
		public int CheckTotalSpace(string drive = null)
		{
			return getTotalDiskSpace();
		}

		/// <summary>
		/// Checks the busy space.
		/// </summary>
		/// <returns>The busy space in MB.</returns>
		public int CheckBusySpace(string drive = null)
		{
			return getBusyDiskSpace();
		}


		[DllImport("diskutils")]
		private static extern int getAvailableDiskSpace();

		[DllImport("diskutils")]
		private static extern int getTotalDiskSpace();

		[DllImport("diskutils")]
		private static extern int getBusyDiskSpace();
	}
}